using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Models.ViewModels;
using QuizMart.Services;
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
        [HttpGet("Get-all-Decks")]
        public async Task<IActionResult> GetAllDecks()
        {
            var decks = await _deckService.GetAllDecks();
            return Ok(decks);
        }
        #endregion

        #region Add Deck
        [HttpPost("Add-Deck")]
        public async Task<IActionResult> AddDeck([FromBody] DeckModel deckModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId, out var hostId))
            {
                deckModel.UserId = hostId; // Ensure the userId in the model is set from the token
                var result = await _deckService.AddDeck(deckModel);
                if (result)
                {
                    return Ok("Deck created successfully.");
                }
                return BadRequest("Failed to create deck.");
            }
            return Unauthorized();
        }
        #endregion

        #region Update Deck
        [HttpPut("Update-Deck")]
        public async Task<IActionResult> UpdateDeck([FromBody] DeckModel deckModel)
        {
            var result = await _deckService.UpdateDeck(deckModel);
            if (result)
            {
                return Ok("Deck updated successfully.");
            }
            return BadRequest("Failed to update deck.");
        }
        #endregion

        #region Delete Deck
        [HttpDelete("Delete-Deck")]
        public async Task<IActionResult> DeleteDeck([FromBody] Guid deckId)
        {
            var result = await _deckService.DeleteDeck(deckId);
            if (result)
            {
                return Ok("Deck deleted successfully.");
            }
            return BadRequest("Failed to delete deck.");
        }
        #endregion
    }
}
