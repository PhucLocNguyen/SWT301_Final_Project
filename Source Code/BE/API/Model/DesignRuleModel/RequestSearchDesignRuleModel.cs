namespace API.Model.DesignRuleModel
{
    public class RequestSearchDesignRuleModel
    {
        public int? TypeOfJewelleryId { get; set; }

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortDesignRuleByEnum sortDesignRuleBy { get; set; }
        public SortDesignRuleTypeEnum sortDesignRuleType { get; set; }
    }

    public enum SortDesignRuleByEnum
    {
        TypeOfJewelleryId = 1,
    }
    public enum SortDesignRuleTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
