using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Services;
using QuizMart.Services.IServices;
using System.Security.Claims;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;        

        public DeckController(IDeckService deckService)
        {
            _deckService = deckService;           
        }

        #region Get All Decks
        [HttpGet]
        public async Task<IActionResult> GetAllDecks()
        {
            try
            {
                var decks = await _deckService.GetAllDecksAsync();
                return Ok(decks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Get All Public Decks
        [HttpGet("public")]
        public async Task<IActionResult> GetAllPublicDecks()
        {
            try
            {
                var decks = await _deckService.GetAllPublicDecksAsync();
                return Ok(decks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Get All My Decks
        [HttpGet("mydecks")]
        public async Task<IActionResult> GetAllMyDecks()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

                if (Guid.TryParse(userIdString, out Guid userId))
                {
                    var decks = await _deckService.GetAllMyDecksAsync(userId);
                    return Ok(decks);
                }
                else
                {
                    return BadRequest("Invalid user ID format.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Add Deck
        [HttpPost]
        public async Task<IActionResult> AddDeck([FromBody] AddDeckVM deck)
        {
            try
            {
                foreach (var quiz in deck.Quizzes)
                {
                    _deckService.ValidateQuizAdd(quiz);
                }
                var hostIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

                if (Guid.TryParse(hostIdString, out Guid hostId))
                {
                    var success = await _deckService.AddDeckAsync(deck, hostId);

                    if (success)
                    {
                        return Ok("Deck created successfully.");
                    }
                    else
                    {
                        return BadRequest("Failed to create Deck.");
                    }
                }
                else
                {
                    return BadRequest("Invalid host ID format.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        #endregion
        [HttpPut("edit-deck")]
        public async Task<IActionResult> EditDeck([FromBody] EditDeckVM deck)
        {
            try
            {
                foreach (var quiz in deck.Quizzes)
                {
                    _deckService.ValidateQuizEdit(quiz);
                }
                var hostIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

                if (Guid.TryParse(hostIdString, out Guid hostId))
                {
                    var success = await _deckService.EditDeckAsync(deck, hostId);
                    if (success)
                    {
                        return Ok("Deck edited successfully.");
                    }
                    else
                    {
                        return NotFound("Deck not found or could not be edited.");
                    }
                }
                else
                {
                    return BadRequest("Invalid host ID format.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        
        #region Delete Deck
        [HttpDelete]
        public async Task<IActionResult> DeleteDeck([FromBody] Guid deckId)
        {
            try
            {
                var result = await _deckService.DeleteDeckAsync(deckId);
                if (result)
                {
                    return Ok("Deck deleted successfully");
                }
                return BadRequest("Deck fail to delete");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Search Deck By Keyword
        [HttpGet("Search-Deck")]
        public async Task<IActionResult> SearchDeckByKeyword([FromQuery] string keyword)
        {
            var deck = await _deckService.SearchDeckByKeyword(keyword);
            return Ok(deck);
        }
        #endregion
       
    }


}
