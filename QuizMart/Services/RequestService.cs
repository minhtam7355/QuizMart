using QuizMart.Models.DomainModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;

namespace QuizMart.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public Task ApproveRequest(Guid requestId, Guid moderatorId) => _requestRepository.ApproveRequest(requestId, moderatorId);

        public Task CreateRequestForDeck(Guid deckId, Guid hostId) => _requestRepository.CreateRequestForDeck(deckId, hostId);

        public Task<ICollection<Request>> GetAllRequests() => _requestRepository.GetAllRequests();
    }
}
