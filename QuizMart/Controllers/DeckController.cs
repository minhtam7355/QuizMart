using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IRequestRepository _requestRepository;
        private IConfiguration _config;
        public DeckController(IDeckRepository deckRepository, IRequestRepository requestRepository, IConfiguration config)
        {
            _deckRepository = deckRepository;
            _requestRepository = requestRepository;
            _config = config;
        }

        #region Get All Decks
        [HttpGet("Get-all-Decks")]
        public async Task<IActionResult> GetAllDecks()
        {
            var decks = await _deckRepository.GetAllDecks();
            return Ok(decks);
        }
        #endregion

        #region Add Deck
        [HttpPost("Add-Deck")]
        public async Task<IActionResult> AddDeck([FromBody] DeckViewModel deck)
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                await _requestRepository.CreateRequestForDeck(Guid.Parse(userID), deck.DeckId);
                return Ok("Request successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Update Deck
        [HttpPut("Update-Deck")]
        public async Task<IActionResult> UpdateDeck([FromBody] Deck deck)
        {
            var result = await _deckRepository.UpdateDeck(deck);
            return Ok(result);
        }
        #endregion

        #region Delete Deck
        [HttpDelete("Delete-Deck")]
        public async Task<IActionResult> DeleteDeck([FromBody] Guid deckId)
        {
            var result = await _deckRepository.DeleteDeck(deckId);
            return Ok(result);
        }
        #endregion

        #region Search Deck By Keyword
        [HttpGet("Search-Deck")]
        public async Task<IActionResult> SearchDeckByKeyword([FromQuery] string keyword)
        {
            var deck = await _deckRepository.SearchDeckByKeyword(keyword);
            return Ok(deck);
        }
        #endregion

    }
}
