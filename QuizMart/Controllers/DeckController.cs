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
        private readonly IRequestService _requestService;

        public DeckController(IDeckService deckService, IRequestService requestService)
        {
            _deckService = deckService;
            _requestService = requestService;
        }

        #region Get All Decks
        [HttpGet("Get-all-Decks")]
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
        [HttpPost("Add-Deck")]
        public async Task<IActionResult> AddDeck([FromBody] DeckModel deckModel)
        {
            try
            {

                await _deckService.AddDeckAsync(deckModel);
                return Ok("Deck created successfully.");

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
                if (result == "Deck updated successfully")
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
