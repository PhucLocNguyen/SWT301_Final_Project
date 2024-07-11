using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Entity;
using Repositories;
using System.Linq.Expressions;
using API.Model.HaveModel;
using API.Model.UserRequirementModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaveController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public HaveController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult SearchHave([FromQuery] RequestSearchHaveModel requestSearchHaveModel)
        {
            var sortBy = requestSearchHaveModel.SortContent != null ? requestSearchHaveModel.SortContent?.sortHaveBy.ToString() : null;
            var sortType = requestSearchHaveModel.SortContent != null ? requestSearchHaveModel.SortContent?.sortHaveType.ToString() : null;
            Expression<Func<Have, bool>> filter = x =>
                (x.RequirementId == requestSearchHaveModel.RequirementId || requestSearchHaveModel.RequirementId == null) &&
                (x.WarrantyCardId == requestSearchHaveModel.WarrantyCardId || requestSearchHaveModel.WarrantyCardId == null);
            Func<IQueryable<Have>, IOrderedQueryable<Have>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortHaveTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortHaveTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }
            var reponseHave = _unitOfWork.HaveRepository.Get(
                filter,
                orderBy,
                includeProperties: "WarrantyCard",
                pageIndex: requestSearchHaveModel.pageIndex,
                pageSize: requestSearchHaveModel.pageSize
                ).Select(x => x.toHaveDTO());
            return Ok(reponseHave);
        }

        [HttpPost]
        public IActionResult CreateHave(RequestCreateHaveModel requestCreateHaveModel)
        {
            var Have = requestCreateHaveModel.ToHaveEntity();
            _unitOfWork.HaveRepository.Insert(Have);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }
        [HttpGet("GetAllWarrantyByRequirementId")]
        public IActionResult GetWarrantyByRequirementId([FromQuery] int requirementId)
        {

            var warrantyInHave = _unitOfWork.HaveRepository.Get(filter: x=>x.RequirementId == requirementId, includes: x=>x.WarrantyCard).Select(x=>x.toHaveDTO()).ToList() ;

            if (warrantyInHave ==null)
            {
                return NotFound("Warranty is not existed");
            }

            return Ok(warrantyInHave);
        }
    }
}
