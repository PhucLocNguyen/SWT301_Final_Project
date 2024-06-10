using API.Model.DesignModel;
using Repositories;

namespace API.Model.MaterialModel
{
    public static class MaterialMapper
    {
        public static Material toMaterialEntity(this RequestCreateMaterialModel requestCreateMaterialModel)
        {
            return new Material()
            {
                Name = requestCreateMaterialModel.Name,
                Price = requestCreateMaterialModel.Price,
                ManagerId = requestCreateMaterialModel.ManagerId,
            };
        }

        public static MaterialDTO toMaterialDTO(this Material material)
        {
            return new MaterialDTO()
            {
                MaterialId = material.MaterialId,
                Name = material.Name,
                Price = material.Price,
                Designs = material.Designs.Select(x => x.toCreateDesign()).ToList()
            };
        }

        public static RequestCreateMaterialModel toCreateMaterial(this Material material)
        {
            return new RequestCreateMaterialModel()
            {
                Name = material.Name,
                Price = (decimal)material.Price,
                ManagerId = material.ManagerId
            };
        }
    }
}
