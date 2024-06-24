namespace API.Model.HaveModel
{
    public class RequestSearchHaveModel
    {
        public int? WarrantyCardId { get; set; }
        public int? RequirementId { get; set; }
        /*public DateTime? DateCreated { get; set; }
        public DateTime? ExpirationDate { get; set; }*/
        public SortContent? SortContent { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = int.MaxValue;
    }

    public class SortContent
    {
        public SortHaveByEnum sortHaveBy { get; set; }
        public SortHaveTypeEnum sortHaveType { get; set; }
    }

    public enum SortHaveByEnum
    {
        WarrantyCardId = 1,
        RequirementId = 2,
        /*DateCreated = 3,
        ExpirationDate = 4*/
    }
    public enum SortHaveTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
