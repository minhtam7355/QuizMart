using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMart.Models.DomainModels;

namespace QuizMart.Repositories
{
    public interface IRequestRepository
    {
        Task<bool> AddRequestAsync(Request request);
        Task<List<Request>> GetAllRequestsAsync();
        Task<Request?> GetRequestByIdAsync(Guid requestId);
        Task<List<Request>> GetAllPendingAddDeckRequestsAsync();
        Task<List<Request>> GetAllPendingEditDeckRequestsAsync();
        Task<bool> UpdateRequestStatusAsync(Guid requestId, bool status, string requestType);
        Task<bool> UpdateRequestAsync(Request request);
    }
}
