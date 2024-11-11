using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class ApplyJob
{
    public int Id { get; set; }

    public int? Jobid { get; set; }

    public int? Userid { get; set; }

    public string? Status { get; set; }

    public string? Resume { get; set; }

    public virtual Job? Job { get; set; }

    public virtual Login? User { get; set; }
}
