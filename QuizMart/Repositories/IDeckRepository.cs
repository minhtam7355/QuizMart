using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public interface IDeckRepository
    {
        Task<ICollection<Deck>> GetAllDecks();
        Task<ICollection<Deck>> GetAllPublicDecks();
        Task<ICollection<Deck>> GetAllMyDecks(Guid userId);
        public Task<bool> AddDeckAsync(Deck deck);
        public Task<bool> UpdateDeckAsync(Deck deck);
        public Task<bool> DeleteDeck(Guid deckId);
        public Task<Deck> SearchDeckByKeyword(string keyword);
        public Task<Deck?> GetDeckByIdAsync(Guid deckId);
    }
}
