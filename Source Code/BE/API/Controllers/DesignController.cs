using API.Model.DesignModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
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
        public IActionResult SearchBlog([FromQuery] RequestSearchDesignModel requestSearchDesignModel)
        {
            var sortBy = requestSearchDesignModel.SortContent != null ? requestSearchDesignModel.SortContent?.sortDesignBy.ToString() : null;
            var sortType = requestSearchDesignModel.SortContent != null ? requestSearchDesignModel.SortContent?.sortDesignType.ToString() : null;
            Expression<Func<Design, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchDesignModel.DesignName) || x.DesignName.Contains(requestSearchDesignModel.DesignName)) &&
                (x.ParentId == requestSearchDesignModel.ParentId || requestSearchDesignModel.ParentId == null) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.Stone) || x.Stone.Kind.Contains(requestSearchDesignModel.Stone)) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.MasterGemstone) || x.MasterGemstone.Kind.Contains(requestSearchDesignModel.MasterGemstone)) &&
                (x.ManagerId == requestSearchDesignModel.ManagerId || requestSearchDesignModel.ManagerId == null) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.TypeOfJewellery) || x.TypeOfJewellery.Name.Contains(requestSearchDesignModel.TypeOfJewellery)) &&
                (string.IsNullOrEmpty(requestSearchDesignModel.Material) || x.Material.Name.Contains(requestSearchDesignModel.Material)) &&
                x.WeightOfMaterial >= requestSearchDesignModel.FromWeightOfMaterial &&
                (x.WeightOfMaterial <= requestSearchDesignModel.ToWeightOfMaterial || requestSearchDesignModel.ToWeightOfMaterial == null);
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
                return NotFound();
            }

            return Ok(Design.toDesignDTO());
        }

        [HttpPost]
        public IActionResult CreateDesign(RequestCreateDesignModel requestCreateDesignModel, int parentId)
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
                if(item.StoneId == requestCreateDesignModel.StoneId && item.MasterGemstoneId == requestCreateDesignModel.MasterGemstoneId 
                    && item.MaterialId == requestCreateDesignModel.MaterialId) 
                {
                    childDesignId = item.DesignId;
                    break;
                }
            }
            if(childDesignId==0)
            {
                var Design = requestCreateDesignModel.toDesignEntity(parentId);
                Design.DesignName = parentDesign.DesignName;
                Design.Image =  parentDesign.Image;
                Design.TypeOfJewelleryId = parentDesign.TypeOfJewelleryId;
                Design.Description = parentDesign.Description;
                Design.WeightOfMaterial = parentDesign.WeightOfMaterial;
                Design.ManagerId = null;
                _unitOfWork.DesignRepository.Insert(Design);
                _unitOfWork.Save();
                return Ok(Design.toDesignDTO);
            }
            else
            {
                var Design = _unitOfWork.DesignRepository.GetByID(childDesignId);
                return Ok(Design);
            }
            
            
        }

        [HttpPut]
        public IActionResult UpdateDesign(int id, RequestCreateDesignModel requestCreateDesignModel)
        {
            var existedDesign = _unitOfWork.DesignRepository.GetByID(id);
            if (existedDesign == null)
            {
                return NotFound();
            }
            existedDesign.ParentId = requestCreateDesignModel.ParentId;
            existedDesign.Image = requestCreateDesignModel.Image;
            existedDesign.DesignName = requestCreateDesignModel.DesignName;
            existedDesign.WeightOfMaterial = requestCreateDesignModel.WeightOfMaterial;
            existedDesign.StoneId = requestCreateDesignModel.StoneId;
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
            var existedDesign = _unitOfWork.DesignRepository.GetByID(id);
            if (existedDesign == null)
            {
                return NotFound();
            }
            _unitOfWork.DesignRepository.Delete(existedDesign);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
