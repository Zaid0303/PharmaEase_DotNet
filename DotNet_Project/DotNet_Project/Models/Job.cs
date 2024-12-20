﻿using System;
using System.Collections.Generic;

namespace DotNet_Project.Models;

public partial class Job
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Department { get; set; }

    public string? Location { get; set; }

    public string? EmpType { get; set; }

    public string? Description { get; set; }

    public string? Qualification { get; set; }

    public string? Experience { get; set; }

    public string? Skills { get; set; }

    public string? ApplicationDateTime { get; set; }

    public string? Salary { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<ApplyJob> ApplyJobs { get; set; } = new List<ApplyJob>();
}
