using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Image { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<ApplyJob> ApplyJobs { get; set; } = new List<ApplyJob>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
