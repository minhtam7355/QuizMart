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
        private readonly IDeckRepository _deckRepository; // Inject Deck Repository

        public RequestService(IRequestRepository requestRepository, IDeckRepository deckRepository)
        {
            _requestRepository = requestRepository;
            _deckRepository = deckRepository;
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

        public async Task<bool> ApproveAddDeckRequestAsync(Guid requestId, Guid modId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.RequestType != "AddDeckRequest")
            {
                return false;
            }

            request.RequestStatus = true;
            request.ModeratorId = modId;

            // Handle nullable DeckId
            if (request.DeckId == null)
            {
                // Handle the scenario where DeckId is null (if necessary)
                return false;
            }

            var deckId = request.DeckId.Value; // Extract the non-nullable Guid value
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck != null)
            {
                deck.Status = "Approved";
                deck.ModeratorId = modId;
                deck.PublishedAt = DateTime.UtcNow;

                // Check the result of UpdateDeckAsync
                var updateSuccess = await _deckRepository.UpdateDeckAsync(deck);
                if (!updateSuccess)
                {
                    return false;
                }
            }

            return await _requestRepository.UpdateRequestAsync(request);
        }

        public async Task<bool> DenyAddDeckRequestAsync(Guid requestId, Guid modId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.RequestType != "AddDeckRequest")
            {
                return false;
            }

            request.RequestStatus = false;
            request.ModeratorId = modId;

            // Handle nullable DeckId
            if (request.DeckId == null)
            {
                // Handle the scenario where DeckId is null (if necessary)
                return false;
            }

            var deckId = request.DeckId.Value; // Extract the non-nullable Guid value
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck != null)
            {
                deck.Status = "Denied";
                deck.ModeratorId = modId;
                deck.PublishedAt = null; // Assuming you want to clear PublishedAt when denied

                await _deckRepository.UpdateDeckAsync(deck);
            }

            return await _requestRepository.UpdateRequestAsync(request);
        }

        public async Task<bool> ApproveEditDeckRequestAsync(Guid requestId, Guid modId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.RequestType != "EditDeckRequest")
            {
                return false;
            }

            request.RequestStatus = true;
            request.ModeratorId = modId;

            // Handle nullable DeckId
            if (request.DeckId == null)
            {
                // Handle the scenario where DeckId is null (if necessary)
                return false;
            }

            var deckId = request.DeckId.Value; // Extract the non-nullable Guid value
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck != null)
            {
                deck.Status = "Approved";
                deck.ModeratorId = modId;
                deck.PublishedAt = DateTime.UtcNow;

                // Check the result of UpdateDeckAsync
                var updateSuccess = await _deckRepository.UpdateDeckAsync(deck);
                if (!updateSuccess)
                {
                    return false;
                }
            }

            return await _requestRepository.UpdateRequestAsync(request);
        }

        public async Task<bool> DenyEditDeckRequestAsync(Guid requestId, Guid modId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.RequestType != "EditDeckRequest")
            {
                return false;
            }

            request.RequestStatus = false;
            request.ModeratorId = modId;

            // Handle nullable DeckId
            if (request.DeckId == null)
            {
                // Handle the scenario where DeckId is null (if necessary)
                return false;
            }

            var deckId = request.DeckId.Value; // Extract the non-nullable Guid value
            var deck = await _deckRepository.GetDeckByIdAsync(deckId);
            if (deck != null)
            {
                deck.Status = "Denied";
                deck.ModeratorId = modId;
                deck.PublishedAt = null; // Assuming you want to clear PublishedAt when denied

                await _deckRepository.UpdateDeckAsync(deck);
            }

            return await _requestRepository.UpdateRequestAsync(request);
        }
    }
}
