using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;

namespace QuizMart.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;

        public DeckService(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository;
        }

        public async Task<string> AddDeckAsync(DeckModel deck)
        {
            
             return await _deckRepository.AddDeck(deck);
            
        }

        public async Task<string> DeleteDeckAsync(Guid deckId)
        {
            return await _deckRepository.DeleteDeck(deckId);
        }

        public async Task<ICollection<DeckModel>> GetAllDecksAsync()
        {
            return await _deckRepository.GetAllDecks();
        }

        public async Task<string> UpdateDeckAsync(DeckModel deck)
        {
            return await _deckRepository.UpdateDeck(deck);
        }
    }
}
