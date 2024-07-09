﻿using AutoMapper;
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

        public Task<string> AddDeck(DeckModel deck)
        {
            try 
            {
                Deck newDeck = _mapper.Map<Deck>(deck);
                _dbContext.Decks.Add(newDeck);
                _dbContext.SaveChanges();
                return Task.FromResult("Deck added successfully");
            }catch(Exception ex)
            {
                return Task.FromResult(ex.Message);
            }            
        }
        public Task<string> DeleteDeck(Guid deckId)
        {
            try
            {
                _dbContext.Decks.Remove(_dbContext.Decks.Find(deckId));
                _dbContext.SaveChanges();
                return Task.FromResult("Deck deleted successfully");
            }catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
        public async Task<ICollection<Deck>> GetAllDecks()
        {
            return await _dbContext.Decks.ToListAsync();
        }

        public async Task<DeckModel> SearchDeckByKeyword(string keyword)
        {
            return await _dbContext.Decks.Where(d => d.Title.Contains(keyword)).Select(d => _mapper.Map<DeckModel>(d)).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateDeck(DeckModel deck)
        {
            // Find the existing deck
            var existingDeck = await _dbContext.Decks.FindAsync(deck.DeckId);

            if (existingDeck != null)
            {
                // Update the properties of the existing deck with the properties of the updated deck
                _mapper.Map(deck, existingDeck);

                // Save the changes
                await _dbContext.SaveChangesAsync();
                return "Deck updated successfully";
            }
            else
            {
                throw new Exception($"Deck not found.");
            }
        }
    }
}
