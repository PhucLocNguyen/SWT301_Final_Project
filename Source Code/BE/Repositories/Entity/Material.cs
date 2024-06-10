using Repositories.Entity;
using System;
using System.Collections.Generic;

namespace Repositories
{

    public partial class Material
    {
        public int MaterialId { get; set; }

        public string Name { get; set; } = null!;

        public decimal? Price { get; set; }

        public string? ManagerId { get; set; }

        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

        public virtual AppUser? Manager { get; set; }
    }
}

