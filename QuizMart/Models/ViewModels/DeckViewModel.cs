namespace QuizMart.Models.ViewModels
{
    public class DeckViewModel
    {
        public Guid DeckId { get; set; }

        public Guid HostId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedAt { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }

        public string Status { get; set; }

        public Guid ModeratorId { get; set; }

    }

}
