namespace QuizMart.Models.ViewModels
{
    public class ChoiceModel
    {
        public Guid ChoiceID { get; set; }
        public Guid QuizID { get; set; }
        public string content { get; set; } 
        public bool IsCorrect {  get; set; } 
    }
}
