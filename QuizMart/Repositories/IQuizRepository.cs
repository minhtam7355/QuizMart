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
        Task AddChoiceAsync(ChoiceModel choice);
        Task<ICollection<Quiz>> GetAllQuizzes();
        Task<ICollection<Choice>> GetAllChoices();
    }
}
