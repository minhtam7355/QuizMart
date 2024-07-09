using System;
using System.Collections.Generic;

namespace QuizMart.Models.DomainModels;

public partial class Choice
{
    public Guid ChoiceId { get; set; }

    public Guid QuizId { get; set; }

    public string? Content { get; set; }

    public bool IsCorrect { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;
}
