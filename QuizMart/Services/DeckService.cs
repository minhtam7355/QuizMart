using AutoMapper;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;

namespace QuizMart.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public DeckService(IDeckRepository deckRepository, IMapper mapper, IRequestService requestService)
        {
            _deckRepository = deckRepository;
            _mapper = mapper;
            _requestService = requestService;
        }

        public async Task<bool> AddDeckAsync(AddDeckVM deck, Guid hostId)
        {
            try
            {
                // Map AddDeckVM to Deck domain model
                var deckDomain = _mapper.Map<Deck>(deck);

                // Assign HostId to the domain model
                deckDomain.HostId = hostId;

                // Assign DeckId to each Quiz in the domain model
                foreach (var quiz in deckDomain.Quizzes)
                {
                    quiz.DeckId = deckDomain.DeckId; // Assign DeckId to Quiz
                    foreach (var choice in quiz.Choices)
                    {
                        choice.QuizId = quiz.QuizId; // Assign QuizId to each Choice
                    }
                }
                if(!await _deckRepository.AddDeckAsync(deckDomain))
                {
                    return false;
                }
                // Call AddDeckRequestAsync from RequestService
                var requestAdded = await _requestService.AddDeckRequestAsync(deckDomain.DeckId, hostId);

                if (!requestAdded)
                {
                    return false; // Optionally handle failure to add request
                }

                // Add deckDomain to the repository (or database)
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> EditDeckAsync(EditDeckVM deck, Guid hostId)
        {
            try
            {
                // Fetch the existing deck from the repository
                var existingDeck = await _deckRepository.GetDeckByIdAsync(deck.DeckId);
                if (existingDeck == null)
                {
                    return false; // Deck not found
                }

                // Map EditDeckVM to the existing Deck domain model
                _mapper.Map(deck, existingDeck);
                existingDeck.Status = null;

                if (!await _deckRepository.UpdateDeckAsync(existingDeck))
                {
                    return false;
                }
                // Call EditDeckRequestAsync from RequestService
                var requestEdited = await _requestService.EditDeckRequestAsync(existingDeck.DeckId, hostId);

                if (!requestEdited)
                {
                    return false; // Optionally handle failure to edit request
                }

                // Edit existingDeck to the repository (or database)
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDeckAsync(Guid deckId)
        {
            return await _deckRepository.DeleteDeck(deckId);
        }

        public async Task<ICollection<DeckModel>> GetAllDecksAsync()
        {
            var decks = await _deckRepository.GetAllDecks();
            var deckModels = _mapper.Map<ICollection<DeckModel>>(decks);
            return deckModels;
        }

        public async Task<ICollection<DeckModel>> GetAllPublicDecksAsync()
        {
            var decks = await _deckRepository.GetAllPublicDecks();
            var deckModels = _mapper.Map<ICollection<DeckModel>>(decks);
            return deckModels;
        }

        public async Task<ICollection<DeckModel>> GetAllMyDecksAsync(Guid userId)
        {
            var decks = await _deckRepository.GetAllMyDecks(userId);
            var deckModels = _mapper.Map<ICollection<DeckModel>>(decks);
            return deckModels;
        }

        public async Task<DeckModel> SearchDeckByKeyword(string keyword)
        {
            var deck = await _deckRepository.SearchDeckByKeyword(keyword);
            var deckModel = _mapper.Map<DeckModel>(deck);
            return deckModel;
        }

        public async Task<bool> UpdateDeckAsync(DeckModel deck)
        {
            try
            {
                var deckEntity = _mapper.Map<Deck>(deck);
                return await _deckRepository.UpdateDeckAsync(deckEntity);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ValidateQuizAdd(AddQuizVM quizModel)
        {

                if (quizModel == null)
                    throw new ArgumentException("Quiz model cannot be null.");

                if (quizModel.Choices == null || !quizModel.Choices.Any())
                    throw new ArgumentException("Quiz must have choices.");

                // Validate choices based on quiz type
                if (quizModel.Type == "MultipleChoices")
                {
                    if (quizModel.Choices.Count <= 2)
                        throw new ArgumentException("Multiple choice quizzes must have more than 2 choices.");
                }
                else if (quizModel.Type == "True/False")
                {
                    if (quizModel.Choices.Count != 2)
                        throw new ArgumentException("True/False quizzes must have exactly 2 choices.");

                    var choiceContents = quizModel.Choices.Select(c => c.Content.ToLower()).ToArray();
                    if (!choiceContents.Contains("true") || !choiceContents.Contains("false"))
                        throw new ArgumentException("True/False quizzes must have choices labeled 'True' and 'False'.");
                }
                else
                {
                    throw new ArgumentException("Invalid quiz type specified. (MultipleChoices or True/False)");
                }

                // Validate choices
                var correctChoicesCount = quizModel.Choices.Count(c => c.IsCorrect);
                if (correctChoicesCount == 0)
                {
                    throw new ArgumentException("At least one choice must be marked as correct.");
                }

                var incorrectChoicesCount = quizModel.Choices.Count(c => !c.IsCorrect);
                if (incorrectChoicesCount == 0)
                {
                    throw new ArgumentException("At least one choice must be marked as incorrect.");
                }
            
        }
        public void ValidateQuizEdit(EditQuizVM quizModel)
        {
            try
            {

                if (quizModel == null)
                    throw new ArgumentException("Quiz model cannot be null.");

                if (quizModel.Choices == null || !quizModel.Choices.Any())
                    throw new ArgumentException("Quiz must have choices.");

                // Validate choices based on quiz type
                if (quizModel.Type == "MultipleChoices")
                {
                    if (quizModel.Choices.Count <= 2)
                        throw new ArgumentException("Multiple choice quizzes must have more than 2 choices.");
                }
                else if (quizModel.Type == "True/False")
                {
                    if (quizModel.Choices.Count != 2)
                        throw new ArgumentException("True/False quizzes must have exactly 2 choices.");

                    var choiceContents = quizModel.Choices.Select(c => c.Content.ToLower()).ToArray();
                    if (!choiceContents.Contains("true") || !choiceContents.Contains("false"))
                        throw new ArgumentException("True/False quizzes must have choices labeled 'True' and 'False'.");
                }
                else
                {
                    throw new ArgumentException("Invalid quiz type specified. (MultipleChoices or True/False)");
                }

                // Validate choices
                var correctChoicesCount = quizModel.Choices.Count(c => c.IsCorrect);
                if (correctChoicesCount == 0)
                {
                    throw new ArgumentException("At least one choice must be marked as correct.");
                }

                var incorrectChoicesCount = quizModel.Choices.Count(c => !c.IsCorrect);
                if (incorrectChoicesCount == 0)
                {
                    throw new ArgumentException("At least one choice must be marked as incorrect.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An unexpected error occurred during validation: {ex.Message}");

                // Optionally, rethrow the exception to be handled by the caller
                throw new ApplicationException("An unexpected error occurred during validation.", ex);
            }
        }
        public void ValidateQuizUpdate(QuizModel quizModel)
        {
            try
            {

                if (quizModel == null)
                    throw new ArgumentException("Quiz model cannot be null.");

                if (quizModel.Choices == null || !quizModel.Choices.Any())
                    throw new ArgumentException("Quiz must have choices.");

                // Validate choices based on quiz type
                if (quizModel.Type == "MultipleChoices")
                {
                    if (quizModel.Choices.Count <= 2)
                        throw new ArgumentException("Multiple choice quizzes must have more than 2 choices.");
                }
                else if (quizModel.Type == "True/False")
                {
                    if (quizModel.Choices.Count != 2)
                        throw new ArgumentException("True/False quizzes must have exactly 2 choices.");

                    var choiceContents = quizModel.Choices.Select(c => c.Content.ToLower()).ToArray();
                    if (!choiceContents.Contains("true") || !choiceContents.Contains("false"))
                        throw new ArgumentException("True/False quizzes must have choices labeled 'True' and 'False'.");
                }
                else
                {
                    throw new ArgumentException("Invalid quiz type specified. (MultipleChoices or True/False)");
                }

                // Validate choices
                var correctChoicesCount = quizModel.Choices.Count(c => c.IsCorrect);
                if (correctChoicesCount == 0)
                {
                    throw new ArgumentException("At least one choice must be marked as correct.");
                }

                var incorrectChoicesCount = quizModel.Choices.Count(c => !c.IsCorrect);
                if (incorrectChoicesCount == 0)
                {
                    throw new ArgumentException("At least one choice must be marked as incorrect.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An unexpected error occurred during validation: {ex.Message}");

                // Optionally, rethrow the exception to be handled by the caller
                throw new ApplicationException("An unexpected error occurred during validation.", ex);
            }
        }
    }
}