using System;
using System.Collections.Generic;

namespace Repositories.Entity
{
    public partial class Stones
    {
        public int StonesId { get; set; }

        public string Kind { get; set; } = null!;

        public decimal Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();
    }
}

