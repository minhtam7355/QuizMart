using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMart.Models.DomainModels;

namespace QuizMart.Services
{
    public interface IRequestService
    {
        Task<bool> AddDeckRequestAsync(Guid deckId, Guid hostId);
        Task<bool> EditDeckRequestAsync(Guid deckId, Guid hostId);
        Task<List<Request>> GetAllRequestsAsync();
        Task<Request?> GetRequestByIdAsync(Guid requestId);
        Task<List<Request>> GetAllPendingAddDeckRequestsAsync();
        Task<List<Request>> GetAllPendingEditDeckRequestsAsync();
        Task<bool> ApproveAddDeckRequestAsync(Guid requestId, Guid modId);
        Task<bool> DenyAddDeckRequestAsync(Guid requestId, Guid modId);
        Task<bool> ApproveEditDeckRequestAsync(Guid requestId);
        Task<bool> DenyEditDeckRequestAsync(Guid requestId);
    }
}
