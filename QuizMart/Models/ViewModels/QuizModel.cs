using QuizMart.Models.DomainModels;

namespace QuizMart.Models.ViewModels
{
    public class QuizModel
    {
        public Guid QuizID { get; set; }
        public Guid DeckID { get; set; }
        public string Type { get; set; }
        public string QuestionText { get; set; }
        public Boolean isFavorite { get; set; }

        public List<ChoiceModel> Choices { get; set; }
    }
}
