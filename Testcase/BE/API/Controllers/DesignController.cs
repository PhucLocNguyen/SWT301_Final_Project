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
        public IActionResult SearchDesign([FromQuery] RequestSearchDesignModel requestSearchDesignModel, int? DesignId)
        {
            try
            {
                var isParent = requestSearchDesignModel.SortContent?.isParent == null ? true: false;
                var sortBy = requestSearchDesignModel.SortContent != null ? requestSearchDesignModel.SortContent?.sortDesignBy.ToString() : null;
                var sortType = requestSearchDesignModel.SortContent != null ? requestSearchDesignModel.SortContent?.sortDesignType.ToString() : null;
                Expression<Func<Design, bool>> filter = x =>
                    (string.IsNullOrEmpty(requestSearchDesignModel.DesignName) || x.DesignName.Contains(requestSearchDesignModel.DesignName)) &&
                    (string.IsNullOrEmpty(requestSearchDesignModel.Stone) || x.Stone.Kind.Contains(requestSearchDesignModel.Stone)) &&
                    (string.IsNullOrEmpty(requestSearchDesignModel.MasterGemstone) || x.MasterGemstone.Kind.Contains(requestSearchDesignModel.MasterGemstone)) &&
                    (x.ManagerId == requestSearchDesignModel.ManagerId || requestSearchDesignModel.ManagerId == null) &&
                    (string.IsNullOrEmpty(requestSearchDesignModel.TypeOfJewellery) || x.TypeOfJewellery.Name.Equals(requestSearchDesignModel.TypeOfJewellery)) &&
                    (string.IsNullOrEmpty(requestSearchDesignModel.Material) || x.Material.Name.Contains(requestSearchDesignModel.Material)) &&
                    ((isParent && x.ParentId == null) || (!isParent)) && ((DesignId > 0 && x.DesignId != DesignId) || !(DesignId > 0));
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
                var reponseDesign = _unitOfWork.DesignRepository.Get(
                    filter,
                    orderBy,
                    pageIndex: requestSearchDesignModel.pageIndex,
                    pageSize: requestSearchDesignModel.pageSize,
                    m => m.Stone, m => m.MasterGemstone, m => m.Material, m => m.TypeOfJewellery
                    ).Select(d => d.toDesignDTO());
                if (DesignId > 0)
                {
                    reponseDesign = reponseDesign.Where(x=>x.DesignId != DesignId).ToList();
                }
                return Ok(reponseDesign);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in SearchDesign");
            }
           
        }

        [HttpGet("{id}")]
        public IActionResult GetDesignById(int id)
        {
            try
            {
                var Design = _unitOfWork.DesignRepository.GetByID(id, m => m.Stone, m => m.MasterGemstone, m => m.Material, m => m.TypeOfJewellery);
                if (Design == null)
                {
                    return NotFound("Design is not existed");
                }

                return Ok(Design.toDesignDTO());
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetDesignById");
            }
            
        }

        [HttpPost("DesignChild")]
        public IActionResult CreateDesignForRequirement(RequestCreateDesignModel requestCreateDesignModel, int parentId)
        {
            try
            {
                var parentDesign = _unitOfWork.DesignRepository.GetByID(parentId);
                int childDesignId = 0;
                var listDesign = _unitOfWork.DesignRepository.Get();
                if (parentDesign == null)
                {
                    return NotFound("ParentId does not exist");
                }
                foreach (var item in listDesign)
                {
                    if (item.StonesId == requestCreateDesignModel.StonesId && item.MasterGemstoneId == requestCreateDesignModel.MasterGemstoneId
                        && item.MaterialId == requestCreateDesignModel.MaterialId)
                    {
                        childDesignId = item.DesignId;
                        break;
                    }
                }
                if (childDesignId == 0)
                {
                    var Design = requestCreateDesignModel.toDesignChildEntity(parentId);
                    Design.DesignName = parentDesign.DesignName;
                    Design.Image = parentDesign.Image;
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
            catch (Exception ex)
            {
                return BadRequest("Create DesignChild failed");
            }
            
        }

        [HttpPost("DesignParent")]
        public IActionResult CreateDesignForManager(RequestCreateDesignModel requestCreateDesignModel)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetByID((int)requestCreateDesignModel.ManagerId, m => m.Role);
                if (user.Role.Name != RoleConst.Manager)
                {
                    return BadRequest("Manager Id is not valid");
                }
                if (requestCreateDesignModel.TypeOfJewelleryId == 0 || requestCreateDesignModel.MaterialId == 0)
                {
                    return BadRequest("Type of Jewellery or Material doesn't allow null");
                }
                var Design = requestCreateDesignModel.toDesignParentEntity();
                _unitOfWork.DesignRepository.Insert(Design);
                _unitOfWork.Save();
                return Ok("Create successfully");
            }
            catch(Exception ex)
            {
                return BadRequest("Create DesignParent failed");
            }
            
        }

        [HttpPut]
        public IActionResult UpdateDesign(int id, RequestCreateDesignModel requestCreateDesignModel)
        {
            try
            {
                var existedDesign = _unitOfWork.DesignRepository.GetByID(id);
                if (existedDesign == null)
                {
                    return NotFound("Design is not existed");
                }
                existedDesign.Image = requestCreateDesignModel.Image;
                existedDesign.Description = requestCreateDesignModel.Description;
                existedDesign.ManagerId = requestCreateDesignModel.ManagerId;   
                _unitOfWork.DesignRepository.Update(existedDesign);
                _unitOfWork.Save();
                return Ok("Update successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong when update design");
            }
           
        }

        [HttpDelete]
        public IActionResult DeleteDesign(int id)
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
            try
            {
                _unitOfWork.DesignRepository.Delete(existedDesign);
                _unitOfWork.Save();
            }
            catch (DbUpdateException ex)
            {
                if (_unitOfWork.IsForeignKeyConstraintViolation(ex))
                {
                    return BadRequest("Cannot delete this item because it is referenced by another entity");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Delete Design successfully");
        }
    }
}
