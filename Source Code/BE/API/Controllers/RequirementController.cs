using API.Model.DesignModel;
using API.Model.RequirementModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequirementController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RequirementController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult SearchBlog([FromQuery] RequestSearchRequirementModel requestSearchRequirementModel)
        {
            var sortBy = requestSearchRequirementModel.SortContent != null ? requestSearchRequirementModel.SortContent?.sortRequirementBy.ToString() : null;
            var sortType = requestSearchRequirementModel.SortContent != null ? requestSearchRequirementModel.SortContent?.sortRequirementType.ToString() : null;
            Expression<Func<Requirement, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchRequirementModel.Status) || x.Status.Contains(requestSearchRequirementModel.Status)) &&
                (string.IsNullOrEmpty(requestSearchRequirementModel.Size) || x.Size.Contains(requestSearchRequirementModel.Size)) &&
                (x.DesignId == requestSearchRequirementModel.DesignId || requestSearchRequirementModel.DesignId == null) &&
                x.ExpectedDelivery <= requestSearchRequirementModel.ToExpectedDelivery &&
                (x.ExpectedDelivery >= requestSearchRequirementModel.FromExpectedDelivery || requestSearchRequirementModel.FromExpectedDelivery == null) && 
                x.GoldPriceAtMoment >= requestSearchRequirementModel.FromGoldPriceAtMoment &&
                (x.GoldPriceAtMoment <= requestSearchRequirementModel.ToGoldPriceAtMoment || requestSearchRequirementModel.ToGoldPriceAtMoment == null) &&
                x.StonePriceAtMoment >= requestSearchRequirementModel.FromStonePriceAtMoment &&
                (x.StonePriceAtMoment <= requestSearchRequirementModel.ToStonePriceAtMoment || requestSearchRequirementModel.ToGoldPriceAtMoment == null) &&
                x.MachiningFee >= requestSearchRequirementModel.FromMachiningFee &&
                (x.MachiningFee <= requestSearchRequirementModel.ToMachiningFee || requestSearchRequirementModel.ToMachiningFee == null) && 
                x.TotalMoney >= requestSearchRequirementModel.FromTotalMoney &&
                (x.TotalMoney <= requestSearchRequirementModel.ToTotalMoney || requestSearchRequirementModel.ToTotalMoney == null);
            Func<IQueryable<Requirement>, IOrderedQueryable<Requirement>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortRequirementTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortDesignTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }
            var reponseDesign = _unitOfWork.RequirementRepository.Get(
                filter,
                orderBy,
                includeProperties: "",
                pageIndex: requestSearchRequirementModel.pageIndex,
                pageSize: requestSearchRequirementModel.pageSize
                ).Select(x=>x.toRequirementDTO());
            return Ok(reponseDesign);
        }

        [HttpGet("{id}")]
        public IActionResult GetRequirementById(int id)
        {
            var Requirement = _unitOfWork.RequirementRepository.GetByID(id);
            if (Requirement == null)
            {
                return NotFound();
            }

            return Ok(Requirement.toRequirementDTO());
        }

        [HttpPost]
        public IActionResult CreateRequirement(RequestCreateRequirementModel requestCreateRequirementModel)
        {
            var Requirement = requestCreateRequirementModel.toRequirementEntity();
            _unitOfWork.RequirementRepository.Insert(Requirement);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateRequirement(int id, RequestCreateRequirementModel requestCreateRequirementModel)
        {
            var existedRequirement = _unitOfWork.RequirementRepository.GetByID(id);
            if (existedRequirement == null)
            {
                return NotFound();
            }
            existedRequirement.Status = requestCreateRequirementModel.Status;
            existedRequirement.ExpectedDelivery = requestCreateRequirementModel.ExpectedDelivery;
            existedRequirement.Size = requestCreateRequirementModel.Size;
            existedRequirement.DesignId = requestCreateRequirementModel.DesignId;
            existedRequirement.Design3D = requestCreateRequirementModel.Design3D;
            existedRequirement.GoldPriceAtMoment = requestCreateRequirementModel.GoldPriceAtMoment;
            existedRequirement.StonePriceAtMoment = requestCreateRequirementModel.StonePriceAtMoment;
            existedRequirement.MachiningFee = requestCreateRequirementModel.MachiningFee;
            existedRequirement.TotalMoney = requestCreateRequirementModel.TotalMoney;
            existedRequirement.CustomerNote = requestCreateRequirementModel.CustomerNote;
            existedRequirement.StaffNote = requestCreateRequirementModel.StaffNote;
            _unitOfWork.RequirementRepository.Update(existedRequirement);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteRequirement(int id)
        {
            var existedRequirement = _unitOfWork.RequirementRepository.GetByID(id);
            if (existedRequirement == null)
            {
                return NotFound();
            }
            _unitOfWork.RequirementRepository.Delete(existedRequirement);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
