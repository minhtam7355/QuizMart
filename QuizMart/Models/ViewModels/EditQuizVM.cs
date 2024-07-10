namespace QuizMart.Models.ViewModels
{
    public class EditQuizVM
    {
        public string? Type { get; set; }

        public string? QuestionText { get; set; }

        public bool Favorite { get; set; }

        public List<EditChoiceVM>? Choices { get; set; }
    }
}
