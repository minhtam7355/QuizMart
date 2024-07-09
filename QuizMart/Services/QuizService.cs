using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;

namespace QuizMart.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        public async Task AddQuizAsync(QuizModel quizModel)
        {
            ValidateQuiz(quizModel);
            var quizEntity = _mapper.Map<Quiz>(quizModel);
            await _quizRepository.AddQuizAsync(quizEntity);
        }
        public async Task<List<QuizModel>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllQuizzes();
            return _mapper.Map<List<QuizModel>>(quizzes);
        }
        public async Task<QuizModel> GetQuizByIdAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(quizId);
            return _mapper.Map<QuizModel>(quiz);
        }
        public async Task DeleteQuizAsync(Guid quizId)
        {
            await _quizRepository.DeleteQuizAsync(quizId);
        }
        public async Task UpdateQuizAsync(QuizModel quizModel)
        {
            await _quizRepository.ExecuteInTransactionAsync(async () =>
            {
                // Fetch the existing quiz from the database including choices
                var existingQuiz = await _quizRepository.GetQuizByIdAsync(quizModel.QuizID);
                if (existingQuiz == null)
                    throw new KeyNotFoundException("Quiz not found.");

                // Validate the incoming model
                ValidateQuiz(quizModel);

                // Remove existing quiz and choices
                await _quizRepository.RemoveChoicesAsync(existingQuiz.Choices);
                await _quizRepository.DeleteQuizAsync(existingQuiz.QuizId);

                // Create new quiz entity with the same QuizId
                var newQuizEntity = _mapper.Map<Quiz>(quizModel);
                newQuizEntity.QuizId = existingQuiz.QuizId;
                newQuizEntity.DeckId = existingQuiz.DeckId;// Reuse the existing QuizId

                // Map new choices
                var newChoices = quizModel.Choices.Select(choiceModel => {
                    var newChoiceEntity = _mapper.Map<Choice>(choiceModel);
                    newChoiceEntity.QuizId = newQuizEntity.QuizId; // Set the foreign key
                    return newChoiceEntity;
                }).ToList();
                newQuizEntity.Choices =  newChoices;
                // Add new quiz and choices
                await _quizRepository.AddQuizAsync(newQuizEntity);
            });
        }
        public async Task<List<QuizModel>> GetAllFavoriteQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllFavoriteQuizzesAsync();
            return _mapper.Map<List<QuizModel>>(quizzes);
        }
        public async Task SetQuizFavoriteStatusAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(quizId);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found.");

            quiz.Favorite = !quiz.Favorite;
            await _quizRepository.UpdateQuizAsync(_mapper.Map<Quiz>(quiz));
        }

        public async Task<bool> CheckChoicesAsync(Guid quizId, List<Guid> choiceIds)
        {
            var correctChoices = await _quizRepository.GetCorrectChoicesByQuizIdAsync(quizId);

            // Check if all provided choiceIds are in the correct choices and their count matches
            return correctChoices.Count == choiceIds.Count && !choiceIds.Except(correctChoices.Select(c => c.ChoiceId)).Any();
        }


        private void ValidateQuiz(QuizModel quizModel)
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
