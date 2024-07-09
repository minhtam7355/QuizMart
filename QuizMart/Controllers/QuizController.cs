using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IChoiceRepository _choiceRepository;

        public QuizzesController(IQuizService quizService, IChoiceRepository choiceRepository)
        {
            _quizService = quizService;
            _choiceRepository = choiceRepository;
        }

        #region Get-all-Quizzez
        [HttpGet]
        [Route("Get-all-Quizzez")]
        public async Task<IActionResult> GetAllQuizzes()
        {
            try
            {
                var quizzes = await _quizService.GetAllQuizzes();
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
            
            try
            {   
                await _quizService.AddQuizAsync(quizModel);
                return Ok("Quiz created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion
        [HttpPut("update/{quizId}")]
        public async Task<IActionResult> UpdateQuiz(Guid quizId, [FromBody] QuizModel quizModel)
        {
            try
            {
                // Assign the QuizId from the route to the quizModel
                quizModel.QuizID = quizId;

                await _quizService.UpdateQuizAsync(quizModel);
                return Ok("Quiz updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("delete/{quizId}")]
        public async Task<IActionResult> DeleteQuiz(Guid quizId)
        {
            try
            {
                await _quizService.DeleteQuizAsync(quizId);
                return Ok("Quiz deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllFavorites")]
        public async Task<IActionResult> GetAllFavorites()
        {
            try
            {
                await _quizService.GetAllFavoriteQuizzesAsync();
                return Ok("Get All Favorite Quizzes Successfully");
            }
            catch(Exception ex) { 
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
              

        }
        [HttpPatch("{quizID}/SetFavorite")]
        public async Task<IActionResult> SetFavoriteStatus(Guid quizID)
        {
            try
            {
                await _quizService.SetQuizFavoriteStatusAsync(quizID);
                return Ok("Set successfully!");
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
    }
}