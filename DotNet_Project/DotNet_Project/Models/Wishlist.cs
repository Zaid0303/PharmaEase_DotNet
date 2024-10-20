using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Wishlist
{
    public int Id { get; set; }

    public int? ProId { get; set; }

    public string? ProName { get; set; }

    public string? Price { get; set; }

    public string? Image { get; set; }

    public DateTime? Date { get; set; }

    public int? Quantity { get; set; }
}
