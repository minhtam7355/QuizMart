using QuizMart.Models.DomainModels;

namespace QuizMart.Models.ViewModels
{
    public class QuizModel
    {
        public Guid QuizId { get; set; }

        public Guid DeckId { get; set; }

        public string? Type { get; set; }

        public string? QuestionText { get; set; }

        public bool Favorite { get; set; }

        public List<ChoiceModel> Choices { get; set; }
    }
}
