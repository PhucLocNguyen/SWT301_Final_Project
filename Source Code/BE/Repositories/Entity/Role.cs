using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
