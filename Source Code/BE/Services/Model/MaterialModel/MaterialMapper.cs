using API.Model.DesignModel;
using Repositories;
using Repositories.Entity;

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
                Image = requestCreateMaterialModel.Image,
            };
        }

        public static ReponseMaterial toMaterialDTO(this Material material)
        {
            return new ReponseMaterial()
            {
                MaterialId = material.MaterialId,
                Name = material.Name,
                Price = material.Price,
                Designs = material.Designs.Select(x => x.toCreateDesign()).ToList(),
                Image = material.Image,
            };
        }

        public static RequestCreateMaterialModel toCreateMaterial(this Material material)
        {
            return new RequestCreateMaterialModel()
            {
                MaterialId = material.MaterialId,
                Name = material.Name,
                Price = (decimal)material.Price,
                ManagerId = material.ManagerId,
                Image = material.Image,
            };
        }
    }
}
