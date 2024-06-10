namespace API.Model.MasterGemstoneModel
{
    public class RequestSearchMasterGemstoneModel
    {
        public string? Kind { get; set; } = null!;

        public decimal? Size { get; set; }
       
        public string? Clarity { get; set; }

        public string? Cut { get; set; }

        public decimal? FromPrice { get; set; } = decimal.Zero;

        public decimal? ToPrice { get; set; }

        public decimal? FromWeight { get; set; } = decimal.Zero ;

        public decimal? ToWeight { get; set; }

        public string? Shape { get; set; }

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortMasterGemstoneByEnum sortMasterGemstoneBy { get; set; }
        public SortMasterGemstoneTypeEnum sortMasterGemstoneType { get; set; }

        public GroupBy groupBy { get; set; }
    }

    public enum SortMasterGemstoneByEnum
    {
        Kind = 1,
        Size = 2,
        Price = 3,
        Clarity = 4,
        Cut = 5,
        Weight = 6,
        Shape = 7,
    }
    public enum SortMasterGemstoneTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }

    public enum GroupBy
    {
        True = 1,
    }
}
