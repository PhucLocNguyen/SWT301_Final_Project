namespace API.Model.RequirementModel
{
    public class RequestSearchRequirementModel
    {
        public string? Status { get; set; }

        public decimal? Size { get; set; }

        public int? DesignId { get; set; }

        public decimal? FromWeightOfMaterial { get; set; } = decimal.Zero;

        public decimal? ToWeightOfMaterial { get; set; }

        public decimal? FromMaterialPriceAtMoment { get; set; } = decimal.Zero;

        public decimal? ToMaterialPriceAtMoment { get; set; }

        public decimal? FromStonePriceAtMoment { get; set; } = decimal.Zero;

        public decimal? ToStonePriceAtMoment { get; set; }

        public decimal? FromMachiningFee { get; set; } = decimal.Zero;

        public decimal? ToMachiningFee { get; set; }

        public decimal? FromTotalMoney { get; set; } = decimal.Zero;

        public decimal? ToTotalMoney { get; set; }

        public SortContent? SortContent { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    public class SortContent
    {
        public SortRequirementByEnum sortRequirementBy { get; set; }
        public SortRequirementTypeEnum sortRequirementType { get; set; }
    }

    public enum SortRequirementByEnum
    {
        DesignId = 1,
        ExpectedDelivery = 2,
        GoldPriceAtMoment = 3,
        StonePriceAtMoment = 4,
        MachiningFee = 5 ,
        TotalMoney = 6 ,
    }
    public enum SortRequirementTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
