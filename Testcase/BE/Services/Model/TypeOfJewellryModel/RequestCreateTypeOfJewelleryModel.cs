using Microsoft.Identity.Client;

namespace API.Model.TypeOfJewellryModel
{
    public class RequestCreateTypeOfJewelleryModel
    {
        public int TypeOfJewelleryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

    }
}
