using QuizMart.Models.DomainModels;

namespace QuizMart.Services.IServices
{
    public interface IRequestService
    {
        Task CreateRequestForDeck(Guid deckId, Guid hostId);
        Task ApproveRequest(Guid requestId, Guid moderatorId);
        Task<ICollection<Request>> GetAllRequests();
    }
}
