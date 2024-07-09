using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Services.IServices
{
    public interface IDeckService
    {
        Task<string> AddDeckAsync(DeckModel deck);
        Task<string> DeleteDeckAsync(Guid deckId);
        Task<ICollection<DeckModel>> GetAllDecksAsync();
        Task<string> UpdateDeckAsync(DeckModel deck);
        public Task<DeckModel> SearchDeckByKeyword(string keyword);
    }
}
