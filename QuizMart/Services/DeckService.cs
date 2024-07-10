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

                // Call AddDeckRequestAsync from RequestService
                var requestAdded = await _requestService.AddDeckRequestAsync(deckDomain.DeckId, hostId);

                if (!requestAdded)
                {
                    return false; // Optionally handle failure to add request
                }

                // Add deckDomain to the repository (or database)
                return await _deckRepository.AddDeckAsync(deckDomain);
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> DeleteDeckAsync(Guid deckId)
        {
            return await _deckRepository.DeleteDeck(deckId);
        }

        public async Task<ICollection<DeckModel>> GetAllDecksAsync()
        {
            var decks = await _deckRepository.GetAllDecks();
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
    }
}