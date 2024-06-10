using Repositories.Entity;

namespace Repositories
{

    public partial class Payment
    {
        public int PaymentId { get; set; }

        public decimal? Amount { get; set; }

        public string? Method { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? CustomerId { get; set; }

        public int? RequirementsId { get; set; }

        public virtual AppUser? Customer { get; set; }

        public virtual Requirement? Requirement { get; set; }
    }

}
