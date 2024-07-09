using QuizMart.Models.DomainModels;

namespace QuizMart.Models.ViewModels
{
    public class DeckModel
    {
        public Guid DeckId { get; set; }

        public Guid HostId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? PublishedAt { get; set; }

        public string? Status { get; set; }

        public Guid? ModeratorId { get; set; }

        public List<QuizModel> Quizzes { get; set; }
    }
}
