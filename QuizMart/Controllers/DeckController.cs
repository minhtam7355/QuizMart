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
            var decks = await _deckService.GetAllDecks();
            return Ok(decks);
        }
        #endregion

        #region Add Deck
        [HttpPost("Add-Deck")]
        public async Task<IActionResult> AddDeck([FromBody] DeckModel deckModel)
        {
            try
            {
                await _deckService.AddDeck(deckModel);
                return Ok("Request successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update Deck
        [HttpPut("Update-Deck")]
        public async Task<IActionResult> UpdateDeck([FromBody] DeckModel deckModel)
        {
            var result = await _deckService.UpdateDeck(deckModel);
            if (result == null)
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
            if (result == null)
            {
                return Ok("Deck deleted successfully.");
            }
            return BadRequest("Failed to delete deck.");
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
