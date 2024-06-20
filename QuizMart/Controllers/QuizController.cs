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

        public QuizzesController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        #region Get-all-Quizzez
        [HttpGet]
        [Route("Get-all-Quizzez")]
        public async Task<IActionResult> GetAllQuizzes()
        {
            try
            {
                var quizzes = await _quizRepository.GetAllQuizzes();
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Create-Quiz
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizModel quizModel)
        {
            if (quizModel == null)
                return BadRequest("Quiz model cannot be null.");

            if (quizModel.Choices == null || quizModel.Choices.Count == 0)
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
        #endregion

        #region Get-all-Choices
        [HttpGet]
        [Route("Get-all-Choices")]
        public async Task<IActionResult> GetAllChoices()
        {
            try
            {
                var choices = await _quizRepository.GetAllChoices();
                return Ok(choices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Create-Choice
        [HttpPost]
        [Route("Create-choice")]
        public async Task<IActionResult> CreateChoice([FromBody] ChoiceModel choiceModel)
        {
            if (choiceModel == null)
                return BadRequest("Choice model cannot be null.");

            try
            {
                await _quizRepository.AddChoiceAsync(choiceModel);
                return Ok("Choice created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

    }
}