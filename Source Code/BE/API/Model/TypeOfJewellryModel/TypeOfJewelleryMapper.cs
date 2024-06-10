using API.Model.DesignModel;
using Repositories;


namespace API.Model.TypeOfJewellryModel
{
    public static class TypeOfJewelleryMapper
    {
        public static TypeOfJewelleryDTO toTypeOfJewelleryDTO(this TypeOfJewellery typeOfJewellery)
        {
            return new TypeOfJewelleryDTO()
            {
                TypeOfJewelleryId = typeOfJewellery.TypeOfJewelleryId,
                Name = typeOfJewellery.Name,
                Designs = typeOfJewellery.Designs.Select(d => d.toCreateDesign()).ToList(),
            };
        }
    }
}
