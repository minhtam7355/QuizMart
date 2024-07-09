namespace QuizMart.Models.ViewModels
{
    public class AddQuizVM
    {
        public string? Type { get; set; }

        public string? QuestionText { get; set; }

        public bool Favorite { get; set; }

        public List<AddChoiceVM>? Choices { get; set; }
    }
}
