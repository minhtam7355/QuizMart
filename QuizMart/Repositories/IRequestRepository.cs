namespace QuizMart.Repositories
{
    public interface IRequestRepository
    {
        Task CreateRequestForDeck(Guid deckId, Guid hostId);
    }
}
