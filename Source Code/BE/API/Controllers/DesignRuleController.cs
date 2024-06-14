using API.Model.DesignModel;
using API.Model.DesignRuleModel;
using API.Model.MasterGemstoneModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignRuleController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public DesignRuleController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult SearchDesignRule([FromQuery] RequestSearchDesignRuleModel requestSearchDesignRuleModel)
        {
            var sortBy = requestSearchDesignRuleModel.SortContent != null ? requestSearchDesignRuleModel.SortContent?.sortDesignRuleBy.ToString() : null;
            var sortType = requestSearchDesignRuleModel.SortContent != null ? requestSearchDesignRuleModel.SortContent?.sortDesignRuleType.ToString() : null;
            Expression<Func<DesignRule, bool>> filter = x =>
                
                (x.TypeOfJewelleryId == requestSearchDesignRuleModel.TypeOfJewelleryId || requestSearchDesignRuleModel.TypeOfJewelleryId == null) ;
            Func<IQueryable<DesignRule>, IOrderedQueryable<DesignRule>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortDesignTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortDesignTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }
            var reponseDesign = _unitOfWork.DesignRuleRepository.Get(
                filter,
                orderBy,
                /*includeProperties: "",*/
                pageIndex: requestSearchDesignRuleModel.pageIndex,
                pageSize: requestSearchDesignRuleModel.pageSize,
                m=>m.TypeOfJewellery
                ).Select(d => d.toDesignRuleDTO());

            return Ok(reponseDesign);
        }

        [HttpGet("{id}")]
        public IActionResult GetDesignRuleById(int id)
        {
            var DesignRule = _unitOfWork.DesignRuleRepository.GetByID(id, m => m.TypeOfJewellery);
            if (DesignRule == null)
            {
                return NotFound();
            }

            return Ok(DesignRule.toDesignRuleDTO());
        }

        [HttpPost]
        public IActionResult CreateDesignRule(RequestCreateDesignRuleModel requestCreateDesignRuleModel)
        {
            if(requestCreateDesignRuleModel.MinSizeMasterGemstone > requestCreateDesignRuleModel.MaxSizeMasterGemstone || 
                requestCreateDesignRuleModel.MinSizeJewellery > requestCreateDesignRuleModel.MaxSizeJewellery)
            {
                return BadRequest();
            }
            var DesignRule = requestCreateDesignRuleModel.toDesignRuleEntity();
            _unitOfWork.DesignRuleRepository.Insert(DesignRule);
            _unitOfWork.Save();
            return Ok("Create successfully");

        }

        [HttpPut]
        public IActionResult UpdateDesignRule(int id, RequestCreateDesignRuleModel requestCreateDesignRuleModel)
        {
            var existedDesignRule = _unitOfWork.DesignRuleRepository.GetByID(id);
            if (existedDesignRule == null)
            {
                return NotFound();
            }
            existedDesignRule.MinSizeMasterGemstone = requestCreateDesignRuleModel.MinSizeMasterGemstone;
            existedDesignRule.MaxSizeMasterGemstone = requestCreateDesignRuleModel.MaxSizeMasterGemstone;
            existedDesignRule.MinSizeJewellery = requestCreateDesignRuleModel.MinSizeJewellery;
            existedDesignRule.MaxSizeJewellery = requestCreateDesignRuleModel.MaxSizeJewellery;
            _unitOfWork.DesignRuleRepository.Update(existedDesignRule);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteDesignRule(int id)
        {
            var existedDesignRule = _unitOfWork.DesignRuleRepository.GetByID(id);
            if (existedDesignRule == null)
            {
                return NotFound();
            }
            _unitOfWork.DesignRuleRepository.Delete(existedDesignRule);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
