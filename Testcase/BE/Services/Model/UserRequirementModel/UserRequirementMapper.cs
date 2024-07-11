using Repositories.Entity;

namespace API.Model.UserRequirementModel
{
    public static class UserRequirementMapper
    {
        public static UserRequirement toUserRequirementEntity(this RequestCreateUserRequirementModel requestCreateUserRequirementModel)
        {
            return new UserRequirement
            {
                UsersId = requestCreateUserRequirementModel.UsersId,
                RequirementId = requestCreateUserRequirementModel.RequirementId
            };
        }
        public static ResponseUserRequirement toUserRequirementDTO(this UserRequirement userRequirement)
        {
            return new ResponseUserRequirement()
            {
                UsersId = userRequirement.UsersId,
                RequirementId = userRequirement.RequirementId,
            };
        }
    }
}
