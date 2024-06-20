using System;
using System.Collections.Generic;

namespace QuizMart.Models.DomainModels;

public partial class Deck
{
    public Guid DeckId { get; set; }

    public Guid HostId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? Status { get; set; }

    public Guid? ModeratorId { get; set; }

    public virtual User Host { get; set; } = null!;

    public virtual User? Moderator { get; set; }

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
