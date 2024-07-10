using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public interface IDeckRepository
    {
        public Task<ICollection<Deck>> GetAllDecks();
        public Task<bool> AddDeckAsync(Deck deck);
        public Task<bool> UpdateDeckAsync(Deck deck);
        public Task<string> DeleteDeck(Guid deckId);
        public Task<Deck> SearchDeckByKeyword(string keyword);
        public Task<Deck?> GetDeckByIdAsync(Guid deckId);
    }
}
