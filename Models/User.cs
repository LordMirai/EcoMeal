using System;
using System.Collections.Generic;

namespace EcoMeal.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool IsDeleted { get; set; }
}
