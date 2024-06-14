using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Users
{
    public int UsersId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Image { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role Role { get; set; }

    public virtual ICollection<UserRequirement> UserRequirements { get; set; } = new List<UserRequirement>();
}
