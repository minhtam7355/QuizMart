using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;

namespace QuizMart.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public async Task AddQuizAsync(QuizModel quiz)
        {
             await _quizRepository.AddQuizAsync(quiz);
        }
        public async Task<List<QuizModel>> GetAllQuizzes()
        {
            return await _quizRepository.GetAllQuizzes();
        }
        public async Task DeleteQuizAsync(Guid quizId)
        {
            await _quizRepository.DeleteQuizAsync(quizId);
        }
        public async Task UpdateQuizAsync(QuizModel quizModel)
        {
             await _quizRepository.UpdateQuizAsync(quizModel);
        }



    }
    
}
