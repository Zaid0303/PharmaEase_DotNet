using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public int? Price { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Strength { get; set; }

    public int? CId { get; set; }

    public virtual Category? CIdNavigation { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
