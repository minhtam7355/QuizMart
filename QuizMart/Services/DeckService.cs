using AutoMapper;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using QuizMart.Repositories;
using QuizMart.Services.IServices;

namespace QuizMart.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IMapper _mapper;

        public DeckService(IDeckRepository deckRepository, IMapper mapper)
        {
            _deckRepository = deckRepository;
            _mapper = mapper;
        }

        public async Task<string> AddDeckAsync(DeckModel deck)
        {
            try
            {
                var deckEntity = _mapper.Map<Deck>(deck);
                var result = await _deckRepository.AddDeck(deckEntity);
                return result;
            }
            catch (Exception ex)
            {
                return $"Error adding deck: {ex.Message}";
            }
        }

        public async Task<string> DeleteDeckAsync(Guid deckId)
        {
            return await _deckRepository.DeleteDeck(deckId);
        }

        public async Task<ICollection<DeckModel>> GetAllDecksAsync()
        {
            var decks = await _deckRepository.GetAllDecks();
            var deckModels = _mapper.Map<ICollection<DeckModel>>(decks);
            return deckModels;
        }

        public async Task<DeckModel> SearchDeckByKeyword(string keyword)
        {
            var deck = await _deckRepository.SearchDeckByKeyword(keyword);
            var deckModel = _mapper.Map<DeckModel>(deck);
            return deckModel;
        }

        public async Task<string> UpdateDeckAsync(DeckModel deck)
        {
            try
            {
                var deckEntity = _mapper.Map<Deck>(deck);
                var result = await _deckRepository.UpdateDeck(deckEntity);
                return result;
            }
            catch (Exception ex)
            {
                return $"Error updating deck: {ex.Message}";
            }
        }
    }
}