using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entity
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();
        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<UserRequirement> UserRequirements { get; set; } = new List<UserRequirement>();
    }
}
