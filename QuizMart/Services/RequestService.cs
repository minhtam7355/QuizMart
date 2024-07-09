using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<bool> AddDeckRequestAsync(Guid deckId, Guid hostId)
        {
            var newRequest = new Request
            {
                RequestId = Guid.NewGuid(),
                RequestType = "AddDeckRequest",
                RequestStatus = null,
                RequestDate = DateTime.UtcNow,
                DeckId = deckId,
                HostId = hostId,
                ModeratorId = null,
            };

            return await _requestRepository.AddRequestAsync(newRequest);
        }

        public async Task<bool> EditDeckRequestAsync(Guid deckId, Guid hostId)
        {
            var newRequest = new Request
            {
                RequestId = Guid.NewGuid(),
                RequestType = "EditDeckRequest",
                RequestStatus = null,
                RequestDate = DateTime.UtcNow,
                DeckId = deckId,
                HostId = hostId,
                ModeratorId = null,
            };

            return await _requestRepository.AddRequestAsync(newRequest);
        }

        public async Task<List<Request>> GetAllRequestsAsync()
        {
            return await _requestRepository.GetAllRequestsAsync();
        }

        public async Task<Request?> GetRequestByIdAsync(Guid requestId)
        {
            return await _requestRepository.GetRequestByIdAsync(requestId);
        }

        public async Task<List<Request>> GetAllPendingAddDeckRequestsAsync()
        {
            return await _requestRepository.GetAllPendingAddDeckRequestsAsync();
        }

        public async Task<List<Request>> GetAllPendingEditDeckRequestsAsync()
        {
            return await _requestRepository.GetAllPendingEditDeckRequestsAsync();
        }
        public async Task<bool> ApproveAddDeckRequestAsync(Guid requestId)
        {
            return await _requestRepository.UpdateRequestStatusAsync(requestId, true, "AddDeckRequest");
        }

        public async Task<bool> DenyAddDeckRequestAsync(Guid requestId)
        {
            return await _requestRepository.UpdateRequestStatusAsync(requestId, false, "AddDeckRequest");
        }

        public async Task<bool> ApproveEditDeckRequestAsync(Guid requestId)
        {
            return await _requestRepository.UpdateRequestStatusAsync(requestId, true, "EditDeckRequest");
        }

        public async Task<bool> DenyEditDeckRequestAsync(Guid requestId)
        {
            return await _requestRepository.UpdateRequestStatusAsync(requestId, false, "EditDeckRequest");
        }
    }
}
