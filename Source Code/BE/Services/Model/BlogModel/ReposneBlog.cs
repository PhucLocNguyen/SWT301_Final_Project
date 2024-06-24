using API.Model.UserModel;
using Repositories.Entity;

namespace API.Model.BlogModel
{
    public class ReposneBlog
    {
        public int BlogId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Image { get; set; }
        public virtual UserDTO? Manager { get; set; }
    }
}
