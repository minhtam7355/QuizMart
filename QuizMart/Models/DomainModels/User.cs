using System;
using System.Collections.Generic;

namespace QuizMart.Models.DomainModels;

public partial class User
{
    public Guid UserId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? HomeAddress { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? ProfilePicture { get; set; }

    public Guid? RoleId { get; set; }

    public virtual ICollection<Deck> DeckHosts { get; set; } = new List<Deck>();

    public virtual ICollection<Deck> DeckModerators { get; set; } = new List<Deck>();

    public virtual Role? Role { get; set; }    
}
