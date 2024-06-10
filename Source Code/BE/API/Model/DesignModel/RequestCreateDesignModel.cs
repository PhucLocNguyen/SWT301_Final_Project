namespace API.Model.DesignModel
{
    public class RequestCreateDesignModel
    {
        public int? ParentId { get; set; }

        public string? Image { get; set; }

        public string? DesignName { get; set; }

        public string? Description { get; set; }

        public decimal? WeightOfMaterial { get; set; }

        public int? StoneId { get; set; }

        public int? MasterGemstoneId { get; set; }

        public string? ManagerId { get; set; }

        public int? TypeOfJewelleryId { get; set; }

        public int? MaterialId { get; set; }

    }
}
