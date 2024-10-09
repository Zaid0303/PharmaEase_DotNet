using System;
using System.Collections.Generic;

namespace PharmaEase.Models;

public partial class Career
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Number { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Qualification { get; set; }

    public string? YearOfQualification { get; set; }

    public string? Resume { get; set; }
}
