using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly QuizMartDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeckRepository(QuizMartDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> AddDeck(DeckModel deckModel)
        {
            try
            {
                var newDeck = _mapper.Map<Deck>(deckModel);
                await _dbContext.Decks.AddAsync(newDeck);
                await _dbContext.SaveChangesAsync();

                return "Deck added successfully";
            }
            catch (Exception ex)
            {
                return $"Error adding deck: {ex.Message}";
            }
        }
        public async Task<string> DeleteDeck(Guid deckId)
        {
            try
            {
                var deck = await _dbContext.Decks.FindAsync(deckId);
                if (deck != null)
                {
                    _dbContext.Decks.Remove(deck);
                    await _dbContext.SaveChangesAsync();
                    return "Deck deleted successfully";
                }
                else
                {
                    return $"Error: Deck with ID {deckId} not found";
                }
            }
            catch (Exception ex)
            {
                return $"Error deleting deck: {ex.Message}";
            }
        }
        public async Task<ICollection<DeckModel>> GetAllDecks()
        {
            var decks = await _dbContext.Decks.ToListAsync();
            return _mapper.Map<ICollection<DeckModel>>(decks);
        }

        // Update an existing deck
        public async Task<string> UpdateDeck(DeckModel deckModel)
        {
            try
            {
                var existingDeck = await _dbContext.Decks.FindAsync(deckModel.DeckId);

                if (existingDeck != null)
                {
                    _mapper.Map(deckModel, existingDeck);
                    await _dbContext.SaveChangesAsync();
                    return "Deck updated successfully";
                }
                else
                {
                    return $"Error: Deck with ID {deckModel.DeckId} not found";
                }
            }
            catch (Exception ex)
            {
                return $"Error updating deck: {ex.Message}";
            }
        }
    }
}
