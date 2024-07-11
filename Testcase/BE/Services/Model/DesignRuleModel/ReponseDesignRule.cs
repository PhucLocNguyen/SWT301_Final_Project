using API.Model.TypeOfJewellryModel;
using Repositories;

namespace API.Model.DesignRuleModel
{
    public class ReponseDesignRule
    {
        public int DesignRuleId { get; set; }

        public decimal? MinSizeMasterGemstone { get; set; }

        public decimal? MaxSizeMasterGemstone { get; set; }

        public decimal? MinSizeJewellery { get; set; }

        public decimal? MaxSizeJewellery { get; set; }

        public virtual RequestCreateTypeOfJewelleryModel? TypeOfJewellery { get; set; }
    }
}
