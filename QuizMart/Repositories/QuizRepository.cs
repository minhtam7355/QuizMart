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

        
    }

}
