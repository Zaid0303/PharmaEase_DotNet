using System;
using System.Collections.Generic;

namespace PharmaEase.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Image { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
