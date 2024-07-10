using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Services.IServices
{
    public interface IDeckService
    {
        Task<bool> AddDeckAsync(AddDeckVM deck, Guid hostId);
        Task<string> DeleteDeckAsync(Guid deckId);
        Task<ICollection<DeckModel>> GetAllDecksAsync();
        Task<bool> UpdateDeckAsync(DeckModel deck);
        public Task<DeckModel> SearchDeckByKeyword(string keyword);

        void ValidateQuizAdd(AddQuizVM quizModel);

        void ValidateQuizUpdate(QuizModel quizModel);
    }
}
