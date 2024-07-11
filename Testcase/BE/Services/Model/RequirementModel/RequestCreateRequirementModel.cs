namespace API.Model.RequirementModel
{
    public class RequestCreateRequirementModel
    {
        public string? Status { get; set; }

        public DateTime? ExpectedDelivery { get; set; } 

        public decimal? Size { get; set; }

        public int? DesignId { get; set; }

        public string? Design3D { get; set; }

        public decimal? WeightOfMaterial { get; set; } = decimal.Zero;

        public decimal? MaterialPriceAtMoment { get; set; } = decimal.Zero;

        public decimal? MasterGemStonePriceAtMoment { get; set; } = decimal.Zero;

        public decimal? StonePriceAtMoment { get; set; }= decimal.Zero;

        public decimal? MachiningFee { get; set; } = decimal.Zero;

        public decimal? TotalMoney { get; set; } = decimal.Zero;

        public string? CustomerNote { get; set; } 

        public string? StaffNote { get; set; }
    }
}
