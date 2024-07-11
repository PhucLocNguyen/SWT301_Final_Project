using API.Model.DesignModel;
using Repositories;
using Repositories.Entity;

namespace API.Model.MasterGemstoneModel
{
    public static class MasterGemstoneMapper
    {
        public static MasterGemstone toMasterGemstonesEntity(this RequestCreateMasterGemstoneModel requestCreateMasterGemstoneModel)
        {
            return new MasterGemstone()
            {
                Kind = requestCreateMasterGemstoneModel.Kind,
                Size = requestCreateMasterGemstoneModel.Size,
                Price = requestCreateMasterGemstoneModel.Price,
                Clarity = requestCreateMasterGemstoneModel.Clarity,
                Cut = requestCreateMasterGemstoneModel.Cut,
                Weight = requestCreateMasterGemstoneModel.Weight,
                Shape = requestCreateMasterGemstoneModel.Shape,
                Image = requestCreateMasterGemstoneModel.Image,
            };
        }
        public static ReponseMasterGemstone toMasterGemstonesDTO(this MasterGemstone masterGemstone)
        {
            return new ReponseMasterGemstone()
            {
                MasterGemstoneId = masterGemstone.MasterGemstoneId,
                Kind = masterGemstone.Kind,
                Size = masterGemstone.Size,
                Price = masterGemstone.Price,
                Clarity = masterGemstone.Clarity,
                Cut = masterGemstone.Cut,
                Weight = masterGemstone.Weight,
                Shape = masterGemstone.Shape,
                Designs = masterGemstone.Designs.Select(x=>x.toCreateDesign()).ToList(),
                Image = masterGemstone.Image,
            };
        }

        public static RequestCreateMasterGemstoneModel toCreateMasterGemstones(this MasterGemstone masterGemstone)
        {
            return new RequestCreateMasterGemstoneModel()
            {
                MasterGemstoneId = masterGemstone.MasterGemstoneId,
                Kind = masterGemstone.Kind,
                Size = (decimal)masterGemstone.Size,
                Price = (decimal)masterGemstone.Price,
                Clarity = masterGemstone.Clarity,
                Cut = masterGemstone.Cut,
                Weight = (decimal)masterGemstone.Weight,
                Shape = masterGemstone.Shape,
                Image = masterGemstone.Image,
            };
        }
    }
}
