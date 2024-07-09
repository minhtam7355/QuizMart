namespace QuizMart.Models.ViewModels
{
    public class ChoiceModel
    {
        public Guid ChoiceId { get; set; }

        public Guid QuizId { get; set; }

        public string? Content { get; set; }

        public bool IsCorrect { get; set; }
    }
}
