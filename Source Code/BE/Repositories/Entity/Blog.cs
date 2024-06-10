using Repositories.Entity;

namespace Repositories
{
    public partial class Blog
    {
        public int BlogId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Image { get; set; }

        public string? ManagerId { get; set; }

        public virtual AppUser? Manager { get; set; }
    }
}

