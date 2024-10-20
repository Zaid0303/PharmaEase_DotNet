using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string? FName { get; set; }

    public string? LName { get; set; }

    public string? Email { get; set; }

    public string? Number { get; set; }

    public string? Message { get; set; }
}
