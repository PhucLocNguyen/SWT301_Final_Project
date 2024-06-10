using API.Model.DesignModel;

namespace API.Model.MaterialModel
{
    public class MaterialDTO
    {
        public int MaterialId { get; set; }
        public string Name { get; set; } = null!;

        public decimal? Price { get; set; }

        public virtual ICollection<RequestCreateDesignModel> Designs { get; set; } = new List<RequestCreateDesignModel>();
    }
}
