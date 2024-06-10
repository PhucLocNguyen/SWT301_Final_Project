using System;
using System.Collections.Generic;

namespace Repositories
{

    public partial class WarrantyCard
    {
        public int WarrantyCardId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<Have> Haves { get; set; } = new List<Have>();
    }

}
