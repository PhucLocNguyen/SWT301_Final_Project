using API.Model.DesignModel;

namespace API.Model.TypeOfJewellryModel
{
    public class ReponseTypeOfJewellery
    {
        public int TypeOfJewelleryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<RequestCreateDesignModel> Designs { get; set; } = new List<RequestCreateDesignModel>();
    }
}
