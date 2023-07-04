using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSVproject.Models;

public partial class Person
{
    public int? Id { get; set; }
    public string? UserId { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Sex { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime DateTime { get; set; }

    public string? JobTitle { get; set; }
}
