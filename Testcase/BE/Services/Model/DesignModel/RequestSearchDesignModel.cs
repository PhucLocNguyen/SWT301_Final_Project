namespace API.Model.DesignModel
{
    public class RequestSearchDesignModel
    {
        public int? ParentId { get; set; }

        public string? Image { get; set; }

        public string? DesignName { get; set; }

        public string? Stone { get; set; }

        public string? MasterGemstone { get; set; }

        public int? ManagerId { get; set; }

        public string? TypeOfJewellery { get; set; }

        public string? Material { get; set; }

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortDesignByEnum sortDesignBy { get; set; }
        public SortDesignTypeEnum sortDesignType { get; set; }
        public IsParent isParent { get; set; }
    }

    public enum SortDesignByEnum
    {
        ParentId = 1,
        DesignName = 2,
        WeightOfMaterial = 3,
        StoneId = 4,
        MasterGemstoneId = 5,
        ManagerId = 6,
        TypeOfJewelleryId = 7,
        MaterialId = 8,
    }
    public enum SortDesignTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }

    public enum IsParent
    {
        IsParent = 1
    }
}
