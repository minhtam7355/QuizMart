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
        #region Add Deck
        [HttpPost]
        public async Task<IActionResult> AddDeck([FromBody] AddDeckVM deck)
        {
            try
            {
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

        #region Update Deck
        [HttpPut("Update-Deck")]
        public async Task<IActionResult> UpdateDeck([FromBody] DeckModel deckModel)
        {
            try
            {
                var result = await _deckService.UpdateDeckAsync(deckModel);
                if (result)
                {
                    return Ok("Deck updated successfully");
                }
                return BadRequest("Deck fail to update");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Delete Deck
        [HttpDelete("Delete-Deck")]
        public async Task<IActionResult> DeleteDeck([FromBody] Guid deckId)
        {
            try
            {
                var result = await _deckService.DeleteDeckAsync(deckId);
                if (result == "Deck deleted successfully")
                {
                    return Ok(result);
                }
                return BadRequest(result);
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
