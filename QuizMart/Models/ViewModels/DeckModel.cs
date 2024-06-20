using QuizMart.Models.DomainModels;

namespace QuizMart.Models.ViewModels
{
    public class DeckModel
    {
        public Guid DeckId { get; set; }
        public Guid UserId { get; set; }
        public string DeckTitle { get; set; }
        public string DeckDescription { get; set;}
        public DateTime Published { get; set; }
        public string Status { get; set; }
        public Guid ModId { get; set; }
        public List<QuizModel> Quizzes { get; set; }
    }
}
