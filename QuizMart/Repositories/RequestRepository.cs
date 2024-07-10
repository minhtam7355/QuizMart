using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMart.Models.DomainModels;
using QuizMart.Context;

namespace QuizMart.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly QuizMartDbContext _dbContext;

        public RequestRepository(QuizMartDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<bool> AddRequestAsync(Request request)
        {
            await _dbContext.Requests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddEditDeckRequestAsync(Guid deckId, Guid hostId)
        {
            var editDeckRequest = new Request
            {
                RequestId = Guid.NewGuid(),
                DeckId = deckId,
                HostId = hostId,
                RequestType = "EditDeckRequest",
                RequestStatus = null, // or false, depending on how you want to handle initial status
                RequestDate = DateTime.UtcNow
            };

            return await AddRequestAsync(editDeckRequest);
        }

        public async Task<List<Request>> GetAllPendingAddDeckRequestsAsync()
        {
            return await _dbContext.Requests
                            .Where(r => r.RequestType == "AddDeckRequest" && (r.RequestStatus == null || r.RequestStatus == false))
                            .ToListAsync();
        }

        public async Task<List<Request>> GetAllPendingEditDeckRequestsAsync()
        {
            return await _dbContext.Requests
                            .Where(r => r.RequestType == "EditDeckRequest" && (r.RequestStatus == null || r.RequestStatus == false))
                            .ToListAsync();
        }

        public async Task<List<Request>> GetAllRequestsAsync()
        {
            return await _dbContext.Requests.ToListAsync();
        }

        public async Task<Request?> GetRequestByIdAsync(Guid requestId)
        {
            return await _dbContext.Requests.FindAsync(requestId);
        }

        public async Task<bool> UpdateRequestStatusAsync(Guid requestId, bool status, string requestType)
        {
            var request = await _dbContext.Requests.FirstOrDefaultAsync(r => r.RequestId == requestId && r.RequestType == requestType);
            if (request == null)
            {
                return false;
            }

            request.RequestStatus = status;
            _dbContext.Requests.Update(request);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateRequestAsync(Request request)
        {
            var existingRequest = await _dbContext.Requests.FindAsync(request.RequestId);
            if (existingRequest == null)
            {
                return false;
            }

            _dbContext.Entry(existingRequest).CurrentValues.SetValues(request);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }     
}
