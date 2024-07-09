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

        public Task<string> AddDeck(DeckModel deck) => _deckRepository.AddDeck(deck);

        public Task<string> DeleteDeck(Guid deckId) => _deckRepository.DeleteDeck(deckId);

        public Task<ICollection<Deck>> GetAllDecks() => _deckRepository.GetAllDecks();

        public Task<DeckModel> SearchDeckByKeyword(string keyword) => _deckRepository.SearchDeckByKeyword(keyword);

        public Task<string> UpdateDeck(DeckModel deck) => _deckRepository.UpdateDeck(deck);
    }
}
