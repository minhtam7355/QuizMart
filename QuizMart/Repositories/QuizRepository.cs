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

            if (quiz.Choices == null || !quiz.Choices.Any())
                throw new ArgumentException("Quiz must have choices.");

            // Validate choices based on quiz type
            if (quiz.Type == "MultipleChoices")
            {
                if (quiz.Choices.Count <= 2)
                    throw new ArgumentException("Multiple choice quizzes must have more than 2 choices.");
            }
            else if (quiz.Type == "True/False")
            {
                if (quiz.Choices.Count != 2)
                    throw new ArgumentException("True/False quizzes must have exactly 2 choices.");

                var choiceContents = quiz.Choices.Select(c => c.Content.ToLower()).ToArray();
                if (!choiceContents.Contains("true") || !choiceContents.Contains("false"))
                    throw new ArgumentException("True/False quizzes must have choices labeled 'True' and 'False'.");
            }
            else
            {
                throw new ArgumentException("Invalid quiz type specified.");
            }
            // Validate choices
            var correctChoicesCount = quiz.Choices.Count(c => c.IsCorrect);
            if (correctChoicesCount == 0)
            {
                throw new ArgumentException("At least one choice must be marked as correct.");
            }
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

    }
}
