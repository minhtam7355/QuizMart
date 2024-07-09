using QuizMart.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMart.Models.DomainModels;

namespace QuizMart.Repositories
{
    public interface IQuizRepository
    {
        Task AddQuizAsync(Quiz quiz);

        Task<List<Quiz>> GetAllQuizzes();

        Task UpdateQuizAsync(Quiz quizModel);

        Task DeleteQuizAsync(Guid quizId);

        Task<List<Quiz>> GetAllFavoriteQuizzesAsync();

        Task<Quiz> GetQuizByIdAsync(Guid quizId);
        Task ExecuteInTransactionAsync(Func<Task> action);
        Task RemoveChoicesAsync(List<Choice> choices);
        Task AddQuizAndChoicesAsync(Quiz quizEntity, List<Choice> choices);

        Task<List<Choice>> GetCorrectChoicesByQuizIdAsync(Guid quizId);
    }
}
