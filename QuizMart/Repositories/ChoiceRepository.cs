using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizMart.Repositories
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly QuizMartDbContext _context;
        private readonly IMapper _mapper;

        public ChoiceRepository(QuizMartDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;

            _mapper = mapper;

        }
        public async Task AddChoiceAsync(ChoiceModel choice)
        {
            try
            {
                var choiceEntity = _mapper.Map<Choice>(choice);

                _context.Choices.Add(choiceEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Rethrow the exception or handle it according to your application logic
                throw new ApplicationException("Error occurred while saving choice to database.", ex);
            }
        }
        public async Task<ICollection<ChoiceModel>> GetAllChoices()
        {
            var choices = await _context.Choices.ToListAsync();
            return _mapper.Map<ICollection<ChoiceModel>>(choices);
        }

    }
}
