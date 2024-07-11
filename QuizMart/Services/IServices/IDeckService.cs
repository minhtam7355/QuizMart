using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Services.IServices
{
    public interface IDeckService
    {
        Task<bool> AddDeckAsync(AddDeckVM deck, Guid hostId);
        Task<bool> EditDeckAsync(EditDeckVM deck, Guid hostId);
        Task<string> DeleteDeckAsync(Guid deckId);
        Task<ICollection<DeckModel>> GetAllDecksAsync();
        Task<ICollection<DeckModel>> GetAllPublicDecksAsync();
        Task<ICollection<DeckModel>> GetAllMyDecksAsync(Guid userId);   
        Task<bool> UpdateDeckAsync(DeckModel deck);
        public Task<DeckModel> SearchDeckByKeyword(string keyword);

        void ValidateQuizAdd(AddQuizVM quizModel);

        void ValidateQuizUpdate(QuizModel quizModel);
        void ValidateQuizEdit(EditQuizVM quizModel);
    }
}
