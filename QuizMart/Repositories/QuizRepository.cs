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
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizMartDbContext _context;
        private readonly IMapper _mapper;

        public QuizRepository(QuizMartDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;

            _mapper = mapper;
        }
        public async Task AddQuizAsync(QuizModel quiz)
        {
            var quizEntity = new Quiz // Assume QuizEntity is the corresponding domain model or entity
            {
                QuizId = quiz.QuizID,
                DeckId = quiz.DeckID,
                Type = quiz.Type,
                QuestionText = quiz.QuestionText,
                Favorite = quiz.isFavorite
            };

            _context.Quizzes.Add(quizEntity);
            await _context.SaveChangesAsync();

            foreach (var choice in quiz.Choices)
            {
                var choiceEntity = new Choice // Assume ChoiceEntity is the corresponding domain model or entity
                {
                    ChoiceId = choice.ChoiceID,
                    QuizId = quiz.QuizID,
                    Content = choice.Content,
                    IsCorrect = choice.IsCorrect
                };
                _context.Choices.Add(choiceEntity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddChoiceAsync(ChoiceModel choice)
        {
            try
            {
                var choiceEntity = new Choice
                {
                    ChoiceId = choice.ChoiceID,
                    QuizId = choice.QuizID,
                    Content = choice.Content,
                    IsCorrect = choice.IsCorrect
                };

                _context.Choices.Add(choiceEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {                
                // Rethrow the exception or return a specific response based on your application logic
                throw new ApplicationException("Error occurred while saving choice to database.", ex);
            }
        }

        public async Task<ICollection<Quiz>> GetAllQuizzes() => await _context.Quizzes.ToListAsync();

        public async Task<ICollection<Choice>> GetAllChoices() => await _context.Choices.ToListAsync();
    }
}
