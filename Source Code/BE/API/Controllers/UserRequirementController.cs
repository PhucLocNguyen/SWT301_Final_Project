using API.Model.UserRequirementModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRequirementController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UserRequirementController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult SearchUserRequirement([FromQuery] RequestSearchUserRequirementModel requestSearchUserRequirementModel)
        {
            var sortBy = requestSearchUserRequirementModel.SortContent?.sortUserRequirementBy.ToString();
            var sortType = requestSearchUserRequirementModel.SortContent?.sortUserRequirementType.ToString();

            Expression<Func<UserRequirement, bool>> filter = x =>
                (x.UsersId == requestSearchUserRequirementModel.UsersId || requestSearchUserRequirementModel.UsersId == 0) &&
                (x.RequirementId == requestSearchUserRequirementModel.RequirementId || requestSearchUserRequirementModel.RequirementId == 0);

            Func<IQueryable<UserRequirement>, IOrderedQueryable<UserRequirement>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortUserRequirementTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortUserRequirementTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }

            var reponseUserRequirement = _unitOfWork.UserRequirementRepository.Get(
                filter,
                orderBy,
                includeProperties: "",
                pageIndex: requestSearchUserRequirementModel.pageIndex,
                pageSize: requestSearchUserRequirementModel.pageSize
            ).Select(ur => ur.toUserRequirementDTO()).ToList();

            return Ok(reponseUserRequirement);
        }

        [HttpPost]
        public IActionResult CreateUserRequirement(RequestCreateUserRequirementModel requestCreateUserRequirementModel)
        {
            var UserRequirementModel = requestCreateUserRequirementModel.toUserRequirementEntity();
            _unitOfWork.UserRequirementRepository.Insert(UserRequirementModel);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }

    }
}
