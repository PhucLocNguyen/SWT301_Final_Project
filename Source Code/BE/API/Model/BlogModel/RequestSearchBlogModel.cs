namespace API.Model.BlogModel
{
    public class RequestSearchBlogModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? ManagerId { get; set; }
        public SortContent? SortContent { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortBlogByEnum sortBlogBy { get; set; }
        public SortBlogTypeEnum sortBlogType { get; set; }
    }

    public enum SortBlogByEnum
    {
        BlogId = 1,
        Title = 2,
        ManagerId = 3,
    }
    public enum SortBlogTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
