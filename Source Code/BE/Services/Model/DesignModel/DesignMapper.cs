using API.Model.MasterGemstoneModel;
using API.Model.MaterialModel;
using API.Model.StonesModel;
using API.Model.TypeOfJewellryModel;
using Repositories;
using Repositories.Entity;

namespace API.Model.DesignModel
{
    public static class DesignMapper
    {
        public static Design toDesignChildEntity(this RequestCreateDesignModel requestCreateDesignModel, int parentId)
        {
            return new Design()
            {
                ParentId = parentId,
                StonesId = requestCreateDesignModel.StonesId,
                MasterGemstoneId = requestCreateDesignModel.MasterGemstoneId,
                ManagerId = requestCreateDesignModel.ManagerId,
                MaterialId = requestCreateDesignModel.MaterialId,
            };
        }

        public static Design toDesignParentEntity(this RequestCreateDesignModel requestCreateDesignModel)
        {
            return new Design()
            {
                ParentId = null,
                StonesId = requestCreateDesignModel.StonesId,
                MasterGemstoneId = requestCreateDesignModel.MasterGemstoneId,
                ManagerId = requestCreateDesignModel.ManagerId,
                MaterialId = requestCreateDesignModel.MaterialId,
                DesignName = requestCreateDesignModel.DesignName,
                Image = requestCreateDesignModel.Image,
                TypeOfJewelleryId = requestCreateDesignModel.TypeOfJewelleryId,
                Description = requestCreateDesignModel.Description,
            };
        }

        public static ReponseDesign toDesignDTO(this Design design)
        {
            return new ReponseDesign()
            {
                DesignId = design.DesignId,
                ParentId = design.ParentId,
                Image = design.Image,
                DesignName = design.DesignName,
                Description = design.Description,
                Stone = design.Stone != null ? StonesMapper.toCreateStones(design.Stone) : null,
                MasterGemstone = design.MasterGemstone != null ? MasterGemstoneMapper.toCreateMasterGemstones(design.MasterGemstone) : null,
                Manager = design.Manager,
                TypeOfJewellery = design.TypeOfJewellery!=null? TypeOfJewelleryMapper.toCreateTypeOfJewellery(design.TypeOfJewellery):null,
                Material = design.Material != null ? MaterialMapper.toCreateMaterial(design.Material) : null,
            };
        }

        public static RequestCreateDesignModel toCreateDesign(this Design design)
        {
            return new RequestCreateDesignModel()
            {
                ParentId = design.ParentId,
                Image = design.Image,
                DesignName = design.DesignName,
                StonesId = design.StonesId,
                MasterGemstoneId = design.MasterGemstoneId,
                ManagerId = design.ManagerId,
                TypeOfJewelleryId = design.TypeOfJewelleryId,
                MaterialId = design.MaterialId,
            };
        }
    }
}
