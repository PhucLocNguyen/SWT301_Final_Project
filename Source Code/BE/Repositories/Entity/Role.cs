using Repositories.Entity;
using System;
using System.Collections.Generic;

namespace Repositories
{

    public partial class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    }

}
