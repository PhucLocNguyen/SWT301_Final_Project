using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entity

{
    public partial class UserRequirement
    {
        public int UsersId { get; set; }
        public int RequirementId { get; set; }
        public virtual Users User { get; set; } = null!;
        public virtual Requirement Requirement { get; set; } = null!;
    }
}
