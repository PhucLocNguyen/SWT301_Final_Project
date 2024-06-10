using System.ComponentModel.DataAnnotations;

namespace API.Model.BlogModel
{
    public class RequestCreateBlogModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string ManagerId { get; set; }
    }
}
