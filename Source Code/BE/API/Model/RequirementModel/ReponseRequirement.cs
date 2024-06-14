namespace API.Model.RequirementModel
{
    public class ReponseRequirement
    {
        public int RequirementId { get; set; }

        public string? Status { get; set; }

        public DateOnly? ExpectedDelivery { get; set; }

        public string? Size { get; set; }

        public int? DesignId { get; set; }

        public string? Design3D { get; set; }

        public decimal? GoldPriceAtMoment { get; set; }

        public decimal? StonePriceAtMoment { get; set; }

        public decimal? MachiningFee { get; set; }

        public decimal? TotalMoney { get; set; }

        public string? CustomerNote { get; set; }

        public string? StaffNote { get; set; }
    }
}
