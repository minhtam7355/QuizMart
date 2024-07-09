﻿using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;

namespace QuizMart.Repositories
{
    public interface IDeckRepository
    {
        public Task<ICollection<Deck>> GetAllDecks();
        public Task<string> AddDeck(DeckModel deck);
        public Task<string> UpdateDeck(DeckModel deck);
        public Task<string> DeleteDeck(Guid deckId);
        public Task<DeckModel> SearchDeckByKeyword(string keyword);
    }
}
