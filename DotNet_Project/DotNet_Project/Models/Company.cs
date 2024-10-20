using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Number { get; set; }

    public string? Location { get; set; }

    public string? Logo { get; set; }
}
