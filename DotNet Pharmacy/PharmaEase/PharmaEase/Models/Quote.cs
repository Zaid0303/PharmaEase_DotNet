using System;
using System.Collections.Generic;

namespace PharmaEase.Models;

public partial class Quote
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Comments { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }
}
