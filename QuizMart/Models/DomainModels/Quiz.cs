using System;
using System.Collections.Generic;

namespace QuizMart.Models.DomainModels;

public partial class Quiz
{
    public Guid QuizId { get; set; }

    public Guid DeckId { get; set; }

    public string? Type { get; set; }

    public string? QuestionText { get; set; }

    public bool? Favorite { get; set; }

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual Deck Deck { get; set; } = null!;
}
