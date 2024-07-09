namespace QuizMart.Models.ViewModels
{
    public class AddDeckVM
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public List<AddQuizVM>? Quizzes { get; set; }
    }
}
