namespace QuizMart.Models.ViewModels
{
    public class EditChoiceVM
    {
        public Guid ChoiceId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
