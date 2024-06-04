﻿using System;
using System.Collections.Generic;

namespace QuizMart.Models.DomainModels;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
