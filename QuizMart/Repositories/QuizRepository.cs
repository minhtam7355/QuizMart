using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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

        public QuizRepository(QuizMartDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddQuizAsync(Quiz quizEntity)
        {
            if (quizEntity == null)
                throw new ArgumentException("Quiz entity cannot be null.");

            _context.Quizzes.Add(quizEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<Quiz> GetQuizByIdAsync(Guid quizId)
        {
            return await _context.Quizzes.Include(q => q.Choices).FirstOrDefaultAsync(q => q.QuizId == quizId);
        }
        public async Task<List<Quiz>> GetAllFavoriteQuizzesAsync()
        {
            return await _context.Quizzes.Include(q => q.Choices).Where(q => q.Favorite).ToListAsync();
        }
        public async Task UpdateQuizAsync(Quiz quizEntity)
        {
            if (quizEntity == null)
                throw new ArgumentException("Quiz entity cannot be null.");

            _context.Quizzes.Update(quizEntity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteQuizAsync(Guid quizId)
        {
            var quizEntity = await _context.Quizzes.Include(q => q.Choices).FirstOrDefaultAsync(q => q.QuizId == quizId);
            if (quizEntity == null)
                throw new KeyNotFoundException("Quiz not found.");

            _context.Quizzes.Remove(quizEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Quiz>> GetAllQuizzes()
        {
            return await _context.Quizzes.Include(q => q.Choices).ToListAsync();
        }

        public async Task RemoveChoicesAsync(List<Choice> choices)
        {
            _context.Choices.RemoveRange(choices);
            await _context.SaveChangesAsync();
        }

        public async Task AddQuizAndChoicesAsync(Quiz quizEntity, List<Choice> choices)
        {
            _context.Quizzes.Add(quizEntity);
            _context.Choices.AddRange(choices);
            await _context.SaveChangesAsync();
        }
        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Choice>> GetCorrectChoicesByQuizIdAsync(Guid quizId)
        {
            return await _context.Choices
                                 .Where(c => c.QuizId == quizId && c.IsCorrect)
                                 .ToListAsync();
        }

        private void ValidateQuiz(QuizModel quizModel)
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
    }

}
