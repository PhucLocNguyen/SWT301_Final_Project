namespace API.Model.MaterialModel
{
    public class RequestSearchMaterialModel
    {
        public string? Name { get; set; } = null!;

        public int? ManagerId { get; set; }

        public decimal? FromPrice { get; set; } = decimal.Zero;

        public decimal? ToPrice { get; set; }

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortMaterialByEnum sortMaterialBy { get; set; }
        public SortMaterialTypeEnum sortMaterialType { get; set; }
    }

    public enum SortMaterialByEnum
    {
        Name = 1,
        Price = 2,
        ManagerId = 3,
    }
    public enum SortMaterialTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
