using QuizMart.Models.ViewModels;

namespace QuizMart.Services
{
    public interface IDeckService
    {
        Task<string> AddDeckAsync(DeckModel deck);
        Task<string> DeleteDeckAsync(Guid deckId);
        Task<ICollection<DeckModel>> GetAllDecksAsync();
        Task<string> UpdateDeckAsync(DeckModel deck);
    }
}
