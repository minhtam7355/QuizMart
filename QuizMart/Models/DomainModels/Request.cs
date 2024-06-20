using System;
using System.Collections.Generic;

namespace QuizMart.Models.DomainModels;

public partial class Request
{
    public Guid RequestId { get; set; }

    public string? RequestType { get; set; }

    public bool? RequestStatus { get; set; }

    public DateTime? RequestDate { get; set; }

    public Guid? DeckId { get; set; }

    public Guid HostId { get; set; }

    public Guid? ModeratorId { get; set; }

    public virtual Deck? Deck { get; set; }

    public virtual User Host { get; set; } = null!;

    public virtual User? Moderator { get; set; }
}
