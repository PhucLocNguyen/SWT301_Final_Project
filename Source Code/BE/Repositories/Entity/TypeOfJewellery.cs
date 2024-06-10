using System;
using System.Collections.Generic;

namespace Repositories
{
    public partial class TypeOfJewellery
    {
        public int TypeOfJewelleryId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<DesignRule> DesignRules { get; set; } = new List<DesignRule>();

        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();
    }

}
