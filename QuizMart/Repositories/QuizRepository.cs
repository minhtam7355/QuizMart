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
            if (quiz == null)
                throw new ArgumentException("Quiz model cannot be null.");
            ValidateQuiz(quiz);
            // Map QuizModel to Quiz entity using AutoMapper
            var quizEntity = _mapper.Map<Quiz>(quiz);

            // Validate and add the quiz entity
            _context.Quizzes.Add(quizEntity);
            await _context.SaveChangesAsync(); // Save changes for the Quiz entity

            

            // Map and add choices for the quiz
            foreach (var choiceModel in quiz.Choices)
            {
                var choiceEntity = _mapper.Map<Choice>(choiceModel);
                choiceEntity.QuizId = quizEntity.QuizId; // Set the foreign key

                _context.Choices.Add(choiceEntity);
            }

            await _context.SaveChangesAsync(); // Save changes for the Choices
        }



        public async Task<List<QuizModel>> GetAllQuizzes()
        {
            var quizzes = await _context.Quizzes.Include(q => q.Choices).ToListAsync();
            return _mapper.Map<List<QuizModel>>(quizzes);
        }
        public async Task UpdateQuizAsync(QuizModel quizModel)
        {
            // Fetch the existing quiz from the database including choices
            var existingQuiz = await _context.Quizzes
                                             .Include(q => q.Choices)
                                             .FirstOrDefaultAsync(q => q.QuizId == quizModel.QuizID);

            if (existingQuiz == null)
                throw new KeyNotFoundException("Quiz not found.");

            // Validate the incoming model
            ValidateQuiz(quizModel);

            // Begin transaction to ensure atomicity
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Remove existing quiz and choices
                _context.Choices.RemoveRange(existingQuiz.Choices);
                _context.Quizzes.Remove(existingQuiz);
                await _context.SaveChangesAsync();

                // Create new quiz entity with the same QuizId
                var newQuizEntity = _mapper.Map<Quiz>(quizModel);
                newQuizEntity.QuizId = existingQuiz.QuizId; // Reuse the existing QuizId

                // Add new choices
                foreach (var choiceModel in quizModel.Choices)
                {
                    var newChoiceEntity = _mapper.Map<Choice>(choiceModel);
                    newChoiceEntity.QuizId = newQuizEntity.QuizId; // Set the foreign key
                    newQuizEntity.Choices.Add(newChoiceEntity);   // Add to the navigation property
                }

                // Add new quiz and choices
                _context.Quizzes.Add(newQuizEntity);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback transaction if there's an error
                await transaction.RollbackAsync();
                throw new ApplicationException("Error updating quiz.", ex);
            }
        }



        public async Task DeleteQuizAsync(Guid quizId)
        {
            var quizEntity = await _context.Quizzes
                                           .Include(q => q.Choices)
                                           .FirstOrDefaultAsync(q => q.QuizId == quizId);

            if (quizEntity == null)
                throw new KeyNotFoundException("Quiz not found.");

            // Remove all associated choices
            foreach (var choice in quizEntity.Choices.ToList())
            {
                _context.Choices.Remove(choice);
            }

            // Remove the quiz itself
            _context.Quizzes.Remove(quizEntity);

            await _context.SaveChangesAsync();
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
