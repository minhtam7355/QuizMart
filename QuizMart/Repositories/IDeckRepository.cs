using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public interface IDeckRepository
    {
        public Task<ICollection<Deck>> GetAllDecks();
        public Task<bool> AddDeckAsync(Deck deck);
        public Task<string> UpdateDeck(Deck deckModel);
        public Task<string> DeleteDeck(Guid deckId);
        public Task<Deck> SearchDeckByKeyword(string keyword);
    }
}
