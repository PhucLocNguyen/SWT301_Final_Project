using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories

{
    public partial class UserRequirement
    {
        public string UsersId { get; set; }
        public int RequirementId { get; set; }
        public virtual AppUser User { get; set; } = null!;
        public virtual Requirement Requirement { get; set; } = null!;
    }
}
