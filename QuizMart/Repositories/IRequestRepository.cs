﻿using QuizMart.Models.DomainModels;

namespace QuizMart.Repositories
{
    public interface IRequestRepository
    {
        Task CreateRequestForDeck(Guid deckId, Guid hostId);
        Task ApproveRequest(Guid requestId, Guid moderatorId);
        Task<ICollection<Request>> GetAllRequests();
    }
}
