namespace QuizMart.Models.ViewModels
{
    public class EditDeckVM
    {

        public Guid DeckId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<EditQuizVM>? Quizzes { get; set; }
    }
}
