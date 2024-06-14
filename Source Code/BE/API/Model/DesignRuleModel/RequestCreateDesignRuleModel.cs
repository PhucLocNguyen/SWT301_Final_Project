namespace API.Model.DesignRuleModel
{
    public class RequestCreateDesignRuleModel
    {
        public int? TypeOfJewelleryId { get; set; }

        public decimal? MinSizeMasterGemstone { get; set; }

        public decimal? MaxSizeMasterGemstone { get; set; }

        public decimal? MinSizeJewellery { get; set; }

        public decimal? MaxSizeJewellery { get; set; }

    }
}
