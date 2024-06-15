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
        private IConfiguration _config;
        public DeckController(IDeckRepository deckRepository, IConfiguration config)
        {
            _deckRepository = deckRepository;
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
            var result = await _deckRepository.AddDeck(deck);
            return Ok(result);
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
    }
}
