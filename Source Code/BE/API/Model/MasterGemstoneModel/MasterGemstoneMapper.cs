using API.Model.DesignModel;
using Repositories;

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
            };
        }
        public static MasterGemstoneDTO toMasterGemstonesDTO(this MasterGemstone masterGemstone)
        {
            return new MasterGemstoneDTO()
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
            };
        }

        public static RequestCreateMasterGemstoneModel toCreateMasterGemstones(this MasterGemstone masterGemstone)
        {
            return new RequestCreateMasterGemstoneModel()
            {
                Kind = masterGemstone.Kind,
                Size = (decimal)masterGemstone.Size,
                Price = (decimal)masterGemstone.Price,
                Clarity = masterGemstone.Clarity,
                Cut = masterGemstone.Cut,
                Weight = (decimal)masterGemstone.Weight,
                Shape = masterGemstone.Shape,
            };
        }
    }
}
