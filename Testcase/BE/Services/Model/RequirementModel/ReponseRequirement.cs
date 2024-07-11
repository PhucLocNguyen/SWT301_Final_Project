namespace API.Model.RequirementModel
{
    public class ReponseRequirement
    {
        public int RequirementId { get; set; }

        public string? Status { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? ExpectedDelivery { get; set; }

        public decimal? Size { get; set; }

        public int? DesignId { get; set; }

        public string? Design3D { get; set; }

        public decimal? WeightOfMaterial { get; set; }

        public decimal? MaterialPriceAtMoment { get; set; }

        public decimal? MasterGemStonePriceAtMoment { get; set; }

        public decimal? StonePriceAtMoment { get; set; }

        public decimal? MachiningFee { get; set; }

        public decimal? TotalMoney { get; set; }

        public string? CustomerNote { get; set; }

        public string? StaffNote { get; set; }
    }
}
