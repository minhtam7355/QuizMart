using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public interface IDeckRepository
    {
        public Task<ICollection<Deck>> GetAllDecks();
        public Task<string> AddDeck(DeckViewModel deck);
        public Task<string> UpdateDeck(Deck deck);
        public Task<string> DeleteDeck(Guid deckId);
    }
}
