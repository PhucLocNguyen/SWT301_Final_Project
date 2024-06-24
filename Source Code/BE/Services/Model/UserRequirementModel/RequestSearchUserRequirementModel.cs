namespace API.Model.UserRequirementModel
{
    public class RequestSearchUserRequirementModel
    {
        public int UsersId { get; set; }
        public int RequirementId { get; set; }
        public SortContent? SortContent { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = int.MaxValue;
    }

    public class SortContent
    {
        public SortUserRequirementByEnum sortUserRequirementBy { get; set; }
        public SortUserRequirementTypeEnum sortUserRequirementType { get; set; }
    }

    public enum SortUserRequirementByEnum
    {
        UsersId = 1,
        RequirementId = 2
    }
    public enum SortUserRequirementTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
