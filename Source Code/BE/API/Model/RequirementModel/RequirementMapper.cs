using Repositories.Entity;

namespace API.Model.RequirementModel
{
    public static class RequirementMapper
    {
        public static Requirement toRequirementEntity(this RequestCreateRequirementModel requestCreateRequirementModel)
        {
            return new Requirement()
            {
                Status = requestCreateRequirementModel.Status,

                ExpectedDelivery = requestCreateRequirementModel.ExpectedDelivery,

                Size = requestCreateRequirementModel.Size,

                DesignId = (int)requestCreateRequirementModel.DesignId,

                Design3D = requestCreateRequirementModel.Design3D,

                MaterialPriceAtMoment = requestCreateRequirementModel.MaterialPriceAtMoment,

                StonePriceAtMoment = requestCreateRequirementModel.StonePriceAtMoment,

                MachiningFee = requestCreateRequirementModel.MachiningFee,

                TotalMoney = requestCreateRequirementModel.TotalMoney,

                CustomerNote = requestCreateRequirementModel.CustomerNote,

                StaffNote = requestCreateRequirementModel.StaffNote,
            };
        }
        public static ReponseRequirement toRequirementDTO(this Requirement requirement)
        {
            return new ReponseRequirement()
            {
                RequirementId = requirement.RequirementId,
                Status = requirement.Status,

                ExpectedDelivery = requirement.ExpectedDelivery,

                Size = requirement.Size,

                DesignId = requirement.DesignId,

                Design3D = requirement.Design3D,

                GoldPriceAtMoment = requirement.MaterialPriceAtMoment,

                StonePriceAtMoment = requirement.StonePriceAtMoment,

                MachiningFee = requirement.MachiningFee,

                TotalMoney = requirement.TotalMoney,

                CustomerNote = requirement.CustomerNote,

                StaffNote = requirement.StaffNote,
            };
        }
    }
}
