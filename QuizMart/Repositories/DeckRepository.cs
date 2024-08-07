﻿using AutoMapper;
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

        public async Task<bool> AddDeckAsync(Deck deck)
        {
            try
            {
                await _context.Decks.AddAsync(deck);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }        

        public async Task<bool> DeleteDeck(Guid deckId)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck == null)
            {
                return false;
            }

            try
            {
                _context.Decks.Remove(deck);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Deck>> GetAllDecks()
        {
            return await _context.Decks.Include(d => d.Quizzes)
                                       .ThenInclude(q => q.Choices)
                                       .ToListAsync();
        }

        public async Task<ICollection<Deck>> GetAllPublicDecks()
        {
            return await _context.Decks.Where(d => d.Status == "Approved")
                                       .Include(d => d.Quizzes)
                                       .ThenInclude(q => q.Choices)
                                       .ToListAsync();
        }

        public async Task<ICollection<Deck>> GetAllMyDecks(Guid userId)
        {
            return await _context.Decks.Where(d => d.HostId == userId).Include(d => d.Quizzes).ThenInclude(q => q.Choices).ToListAsync();
        }

        public async Task<Deck?> GetDeckByIdAsync(Guid deckId)
        {
            return await _context.Decks.FindAsync(deckId);
        }

        public async Task<Deck> SearchDeckByKeyword(string keyword)
        {
            return await _context.Decks.FirstOrDefaultAsync(d => d.Title.Contains(keyword));
        }

        public async Task<bool> UpdateDeckAsync(Deck deck)
        {
            var existingDeck = await _context.Decks.FindAsync(deck.DeckId);
            if (existingDeck == null)
            {
                return false;
            }

            _context.Entry(existingDeck).CurrentValues.SetValues(deck);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}