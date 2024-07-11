namespace API.Model.TypeOfJewellryModel
{
    public class RequestSearchTypeOfJewelleryModel
    {
        public string? Name { get; set; }

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortTypeOfJewelleryByEnum sortTypeOfJewelleryBy { get; set; }
        public SortTypeOfJewelleryTypeEnum sortTypeOfJewelleryType { get; set; }
    }

    public enum SortTypeOfJewelleryByEnum
    {
        Name = 1,
    }
    public enum SortTypeOfJewelleryTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
