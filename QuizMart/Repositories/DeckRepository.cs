using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly QuizMartDbContext _context;

        public DeckRepository(QuizMartDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddDeck(Deck deck)
        {
            try
            {
                _context.Decks.Add(deck);
                await _context.SaveChangesAsync();
                return "Deck added successfully.";
            }
            catch (Exception ex)
            {
                return $"Error adding deck: {ex.Message}";
            }
        }        

        public async Task<string> DeleteDeck(Guid deckId)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck == null)
            {
                return "Deck not found.";
            }

            try
            {
                _context.Decks.Remove(deck);
                await _context.SaveChangesAsync();
                return "Deck deleted successfully.";
            }
            catch (Exception ex)
            {
                return $"Error deleting deck: {ex.Message}";
            }
        }

        public async Task<ICollection<Deck>> GetAllDecks()
        {
            return await _context.Decks.ToListAsync();
        }

        public async Task<Deck> SearchDeckByKeyword(string keyword)
        {
            return await _context.Decks.FirstOrDefaultAsync(d => d.Title.Contains(keyword));
        }

        public async Task<string> UpdateDeck(Deck deck)
        {
            var existingDeck = await _context.Decks.FindAsync(deck.DeckId);
            if (existingDeck == null)
            {
                return "Deck not found.";
            }

            try
            {
                _context.Entry(existingDeck).CurrentValues.SetValues(deck);
                await _context.SaveChangesAsync();
                return "Deck updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error updating deck: {ex.Message}";
            }
        }
    }
}