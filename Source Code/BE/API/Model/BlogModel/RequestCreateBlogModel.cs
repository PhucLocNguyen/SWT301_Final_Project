using System.ComponentModel.DataAnnotations;

namespace API.Model.BlogModel
{
    public class RequestCreateBlogModel
    {
        public string Title { get; set; } = null;
        public string Description { get; set; } = null;
        public string? Image { get; set; } = null;
        public int ManagerId { get; set; }
    }
}
