using API.Model.DesignModel;

namespace API.Model.StonesModel
{
    public class StonesDTO
    {
        public int StonesId { get; set; }
        public string Kind { get; set; } = null!;

        public decimal? Size { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public virtual ICollection<RequestCreateDesignModel> Designs { get; set; } = new List<RequestCreateDesignModel>();
    }
}
