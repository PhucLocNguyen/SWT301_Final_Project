namespace API.Model.PaymentModel
{
    public class RequestSearchPaymentModel
    {
        public string? Method { get; set; }

        public int? CustomerId { get; set; }

        public int? RequirementsId { get; set; }

        public decimal? FromAmount { get; set; } = decimal.Zero;

        public decimal? ToAmount { get; set; } 

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortPaymentByEnum sortPaymentBy { get; set; }
        public SortPaymentTypeEnum sortPaymentType { get; set; }
    }

    public enum SortPaymentByEnum
    {
        Method = 1,
        CustomerId = 2,
        RequirementsId = 3,
        Amount = 4
    }
    public enum SortPaymentTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
