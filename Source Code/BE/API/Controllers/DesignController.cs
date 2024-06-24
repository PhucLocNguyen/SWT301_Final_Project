using API.Model.BlogModel;
using API.Model.DesignModel;
using API.Model.UserModel;
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
    public class DesignController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public DesignController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult SearchDesign([FromQuery] RequestSearchDesignModel requestSearchDesignModel)
        {
            var sortBy = requestSearchDesignModel.SortContent != null ? requestSearchDesignModel.SortContent?.sortDesignBy.ToString() : null;
            var sortType = requestSearchDesignModel.SortContent != null ? requestSearchDesignModel.SortContent?.sortDesignType.ToString() : null;
            Expression<Func<Design, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchDesignModel.DesignName) || x.DesignName.Contains(requestSearchDesignModel.DesignName)) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.Stone) || x.Stone.Kind.Contains(requestSearchDesignModel.Stone)) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.MasterGemstone) || x.MasterGemstone.Kind.Contains(requestSearchDesignModel.MasterGemstone)) &&
                (x.ManagerId == requestSearchDesignModel.ManagerId || requestSearchDesignModel.ManagerId == null) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.TypeOfJewellery) || x.TypeOfJewellery.Name.Contains(requestSearchDesignModel.TypeOfJewellery)) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.Material) || x.Material.Name.Contains(requestSearchDesignModel.Material)) ;
            Func<IQueryable<Design>, IOrderedQueryable<Design>> orderBy = null;

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
            if(requestSearchDesignModel.SortContent?.isParent == null)
            {
                filter = x => (x.ParentId == null);
            }else
            {
                filter = x => (x.ParentId == requestSearchDesignModel.ParentId || requestSearchDesignModel.ParentId == null);
            }
            var reponseDesign = _unitOfWork.DesignRepository.Get(
                filter,
                orderBy,
                /*includeProperties: "",*/
                pageIndex: requestSearchDesignModel.pageIndex,
                pageSize: requestSearchDesignModel.pageSize,
                m => m.Stone, m => m.MasterGemstone, m => m.Material, m => m.TypeOfJewellery
                ).Select(d => d.toDesignDTO());
            return Ok(reponseDesign);
        }

        [HttpGet("{id}")]
        public IActionResult GetDesignById(int id)
        {
            var Design = _unitOfWork.DesignRepository.GetByID(id, m => m.Stone, m => m.MasterGemstone, m => m.Material, m => m.TypeOfJewellery);
            if (Design == null)
            {
                return NotFound("Design is not existed");
            }

            return Ok(Design.toDesignDTO());
        }

        [HttpPost("DesignChild")]
        public IActionResult CreateDesignForRequirement(RequestCreateDesignModel requestCreateDesignModel, int parentId)
        {
            var parentDesign = _unitOfWork.DesignRepository.GetByID(parentId);
            int childDesignId = 0;
            var listDesign = _unitOfWork.DesignRepository.Get();
            if(parentDesign == null)
            {
                return NotFound("ParentId does not exist");
            }
            foreach (var item in listDesign)
            {
                if(item.StonesId == requestCreateDesignModel.StonesId && item.MasterGemstoneId == requestCreateDesignModel.MasterGemstoneId 
                    && item.MaterialId == requestCreateDesignModel.MaterialId) 
                {
                    childDesignId = item.DesignId;
                    break;
                }
            }
            if(childDesignId==0)
            {
                var Design = requestCreateDesignModel.toDesignChildEntity(parentId);
                Design.DesignName = parentDesign.DesignName;
                Design.Image =  parentDesign.Image;
                Design.TypeOfJewelleryId = parentDesign.TypeOfJewelleryId;
                Design.Description = parentDesign.Description;
                Design.ManagerId = null;
                _unitOfWork.DesignRepository.Insert(Design);
                _unitOfWork.Save();
                return Ok(Design.toDesignDTO());
                /*return Ok("Create successfully");*/
            }
            else
            {
                var Design = _unitOfWork.DesignRepository.GetByID(childDesignId, m => m.Stone, m => m.MasterGemstone, m => m.Material, m => m.TypeOfJewellery); ;
                return Ok(Design.toDesignDTO());
            }
        }

        [HttpPost("DesignParent")]
        public IActionResult CreateDesignForManager(RequestCreateDesignModel requestCreateDesignModel)
        {
            var user = _unitOfWork.UserRepository.GetByID((int)requestCreateDesignModel.ManagerId, m => m.Role);
            if (user.Role.Name != RoleConst.Manager)
            {
                return BadRequest("Manager Id is not valid");
            }
            var Design = requestCreateDesignModel.toDesignParentEntity();
            _unitOfWork.DesignRepository.Insert(Design);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }

        [HttpPut]
        public IActionResult UpdateDesign(int id, RequestCreateDesignModel requestCreateDesignModel)
        {
            var existedDesign = _unitOfWork.DesignRepository.GetByID(id);
            if (existedDesign == null)
            {
                return NotFound("Design is not existed");
            }
            existedDesign.ParentId = requestCreateDesignModel.ParentId;
            existedDesign.Image = requestCreateDesignModel.Image;
            existedDesign.DesignName = requestCreateDesignModel.DesignName;
            existedDesign.StonesId = requestCreateDesignModel.StonesId;
            existedDesign.MasterGemstoneId = requestCreateDesignModel.MasterGemstoneId;
            existedDesign.ManagerId = requestCreateDesignModel.ManagerId;
            existedDesign.TypeOfJewelleryId = (int)requestCreateDesignModel.TypeOfJewelleryId;
            existedDesign.MaterialId = requestCreateDesignModel.MaterialId;
            _unitOfWork.DesignRepository.Update(existedDesign);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            Expression<Func<Design, bool>> filter = x =>
                (x.ParentId == id);
            var countChild = _unitOfWork.DesignRepository.Count(filter);
            if(countChild > 0)
            {
                return BadRequest("The Design Id is existed in another Design");
            }
            var existedDesign = _unitOfWork.DesignRepository.GetByID(id);
            if (existedDesign == null)
            {
                return NotFound("Design is not existed");
            }
            _unitOfWork.DesignRepository.Delete(existedDesign);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
