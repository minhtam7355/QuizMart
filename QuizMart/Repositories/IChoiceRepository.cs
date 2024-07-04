using QuizMart.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMart.Models.DomainModels;

namespace QuizMart.Repositories
{
    public interface IChoiceRepository
    {
        Task AddChoiceAsync(ChoiceModel choice);
        Task<ICollection<ChoiceModel>> GetAllChoices();
    }
}