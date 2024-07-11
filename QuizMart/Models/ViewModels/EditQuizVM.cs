namespace QuizMart.Models.ViewModels
{
    public class EditQuizVM
    {
        public Guid QuizId { get; set; }
        public string? Type { get; set; }

        public string? QuestionText { get; set; }

        public bool Favorite { get; set; }

        public List<EditChoiceVM>? Choices { get; set; }
    }
}
