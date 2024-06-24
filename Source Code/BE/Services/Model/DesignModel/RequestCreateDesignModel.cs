using Repositories.Entity;

namespace API.Model.DesignModel
{
    public class RequestCreateDesignModel
    {
        public int? ParentId { get; set; } = null;

        public string? Image { get; set; }

        public string? DesignName { get; set; }

        public string? Description { get; set; }

        public int? StonesId { get; set; }

        public int? MasterGemstoneId { get; set; }

        public int? ManagerId { get; set; }

        public int? TypeOfJewelleryId { get; set; }

        public int? MaterialId { get; set; }
     
    }
}
