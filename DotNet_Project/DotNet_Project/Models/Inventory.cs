using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Price { get; set; }

    public string? Quantity { get; set; }

    public int? CId { get; set; }

    public string? Availability { get; set; }

    public string? Description { get; set; }
}
