using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public int Proid { get; set; }

    public int? Proprice { get; set; }

    public int? Qty { get; set; }

    public virtual Product Pro { get; set; } = null!;

    public virtual Login User { get; set; } = null!;
}
