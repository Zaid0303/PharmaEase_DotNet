using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Quantity { get; set; }

    public string? Price { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Strength { get; set; }

    public int? CId { get; set; }

    public virtual Category? CIdNavigation { get; set; }
}
