using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;

namespace QuizMart.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly QuizMartDbContext _context;
        private readonly IMapper _mapper;
        public RequestRepository(QuizMartDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task CreateRequestForDeck(Guid userId, Guid deckId)
        {
            // Create a new request
            var request = new Request
            {
                RequestId = Guid.NewGuid(),
                RequestType = "AddDeck", // Customize as needed
                RequestStatus = null, // Pending initially
                RequestDate = DateTime.UtcNow,
                DeckId = deckId,
                HostId = userId,
                ModeratorId = null // No moderator assigned yet
            };

            // Mark the deck as pending
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck != null)
            {
                deck.Status = "Pending";
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveRequest(Guid requestId, Guid moderatorId)
        {
            // Find the request
            var request = await _context.Requests.FindAsync(requestId);
            if (request != null)
            {
                // Update the request status to approved
                request.RequestStatus = true;
                request.ModeratorId = moderatorId;

                // Update the deck status to approved
                var deck = await _context.Decks.FindAsync(request.DeckId);
                if (deck != null)
                {
                    deck.Status = "Approved";
                    await _context.SaveChangesAsync();
                }
                else
                {
                    deck.Status = "Rejected";
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<ICollection<Request>> GetAllRequests()
        {
            return await _context.Requests.ToListAsync();
        }
    }
}
