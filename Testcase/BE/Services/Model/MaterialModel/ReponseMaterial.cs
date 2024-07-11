using API.Model.DesignModel;

namespace API.Model.MaterialModel
{
    public class ReponseMaterial
    {
        public int MaterialId { get; set; }

        public string Name { get; set; } = null!;

        public decimal? Price { get; set; }

        public string Image { get; set; }

        public int ManagerId { get; set; }

        public virtual ICollection<RequestCreateDesignModel> Designs { get; set; } = new List<RequestCreateDesignModel>();
    }
}
