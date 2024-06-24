using API.Model.MasterGemstoneModel;
using API.Model.TypeOfJewellryModel;
using Repositories;
using Repositories.Entity;

namespace API.Model.DesignRuleModel
{
    public static class ConversationMapper
    {
        public static DesignRule toDesignRuleEntity(this RequestCreateDesignRuleModel requestCreateDesignRuleModel)
        {
            return new DesignRule()
            {
                MinSizeMasterGemstone = requestCreateDesignRuleModel.MinSizeMasterGemstone,
                MaxSizeMasterGemstone = requestCreateDesignRuleModel.MaxSizeMasterGemstone,
                MinSizeJewellery = requestCreateDesignRuleModel.MinSizeJewellery,
                MaxSizeJewellery = requestCreateDesignRuleModel.MaxSizeJewellery,
                TypeOfJewelleryId = (int)requestCreateDesignRuleModel.TypeOfJewelleryId,
            };
        }
        public static ReponseDesignRule toDesignRuleDTO(this DesignRule designRule)
        {
            return new ReponseDesignRule()
            {
                DesignRuleId = designRule.DesignRuleId,
                MinSizeMasterGemstone = designRule.MinSizeMasterGemstone,
                MaxSizeMasterGemstone = designRule.MaxSizeMasterGemstone,
                MinSizeJewellery = designRule.MinSizeJewellery,
                MaxSizeJewellery = designRule.MaxSizeJewellery,
                TypeOfJewellery = new RequestCreateTypeOfJewelleryModel() { Name = designRule.TypeOfJewellery.Name,
                                                                            Image = designRule.TypeOfJewellery.Image},
            };
        }
    }
}
