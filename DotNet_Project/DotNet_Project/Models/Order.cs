using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? TotalAmount { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Date { get; set; }

    public string? CustomerName { get; set; }

    public string? OrderStatus { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Login? User { get; set; }
}
