namespace API.Model.StonesModel
{
    public class RequestSearchStonesModel
    {
        public string? Kind { get; set; }

        public decimal? Size { get; set; }

        public int? FromQuantity { get; set; } = 0;

        public int? ToQuantity { get; set; }

        public decimal? FromPrice { get; set; } = decimal.Zero;

        public decimal? ToPrice { get; set; }

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortStonesByEnum sortStonetBy { get; set; }
        public SortStonesTypeEnum sortStonesType { get; set; }
    }

    public enum SortStonesByEnum
    {
        Kind = 1,
        Quantity = 2,
        Price = 3,
    }
    public enum SortStonesTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
