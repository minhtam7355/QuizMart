using QuizMart.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMart.Models.DomainModels;

namespace QuizMart.Repositories
{
    public interface IQuizRepository
    {
        Task AddQuizAsync(QuizModel quiz);

        Task<List<QuizModel>> GetAllQuizzes();

        Task UpdateQuizAsync(QuizModel quizModel);

        Task DeleteQuizAsync(Guid quizId);
    }
}
