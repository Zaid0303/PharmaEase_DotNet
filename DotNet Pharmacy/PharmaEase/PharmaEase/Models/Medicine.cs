using System;
using System.Collections.Generic;

namespace PharmaEase.Models;

public partial class Medicine
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Price { get; set; }

    public int? Quantity { get; set; }

    public int? CId { get; set; }

    public string? Strength { get; set; }

    public string? Availability { get; set; }

    public string? Description { get; set; }
}
