using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IChoiceRepository _choiceRepository;

        public QuizzesController(IQuizRepository quizRepository, IChoiceRepository choiceRepository)
        {
            _quizRepository = quizRepository;
            _choiceRepository = choiceRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizModel quizModel)
        {
            if (quizModel == null)
                return BadRequest("Quiz model cannot be null.");

            if (quizModel.choices == null || quizModel.choices.Count == 0)
                return BadRequest("Quiz must have at least one choice.");

            try
            {
                await _quizRepository.AddQuizAsync(quizModel);
                return Ok("Quiz created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}