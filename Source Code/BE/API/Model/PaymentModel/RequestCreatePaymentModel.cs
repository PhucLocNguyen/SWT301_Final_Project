namespace API.Model.PaymentModel
{
    public class RequestCreatePaymentModel
    {
        public decimal? Amount { get; set; }

        public string? Method { get; set; }

        public int? CustomerId { get; set; }

        public int? RequirementsId { get; set; }
    }
}
