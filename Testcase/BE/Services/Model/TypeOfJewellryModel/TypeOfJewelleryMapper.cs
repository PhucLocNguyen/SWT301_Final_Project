using API.Model.DesignModel;
using Repositories.Entity;


namespace API.Model.TypeOfJewellryModel
{
    public static class TypeOfJewelleryMapper
    {
        public static ReponseTypeOfJewellery toTypeOfJewelleryDTO(this TypeOfJewellery typeOfJewellery)
        {
            return new ReponseTypeOfJewellery()
            {
                TypeOfJewelleryId = typeOfJewellery.TypeOfJewelleryId,
                Name = typeOfJewellery.Name,
                Designs = typeOfJewellery.Designs.Select(d => d.toCreateDesign()).ToList(),
                Image = typeOfJewellery.Image,
            };
        }

        public static RequestCreateTypeOfJewelleryModel toCreateTypeOfJewellery(this TypeOfJewellery typeOfJewellery)
        {
            return new RequestCreateTypeOfJewelleryModel()
            {
                TypeOfJewelleryId = typeOfJewellery.TypeOfJewelleryId,
                Name = typeOfJewellery.Name,
                Image = typeOfJewellery.Image,
            };
        }
    }
}
