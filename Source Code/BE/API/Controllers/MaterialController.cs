using API.Model.MaterialModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MaterialController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult SearchMaterial([FromQuery] RequestSearchMaterialModel requestSearchMaterialModel)
        {
            var sortBy = requestSearchMaterialModel.SortContent != null ? requestSearchMaterialModel.SortContent?.sortMaterialBy.ToString() : null;
            var sortType = requestSearchMaterialModel.SortContent != null ? requestSearchMaterialModel.SortContent?.sortMaterialType.ToString() : null;
            Expression<Func<Material, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchMaterialModel.Name) || x.Name.Contains(requestSearchMaterialModel.Name)) &&
                (x.ManagerId == requestSearchMaterialModel.ManagerId || requestSearchMaterialModel.ManagerId == null) &&
                x.Price >= requestSearchMaterialModel.FromPrice &&
                (x.Price <= requestSearchMaterialModel.ToPrice || requestSearchMaterialModel.ToPrice == null);
            Func<IQueryable<Material>, IOrderedQueryable<Material>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortMaterialTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortMaterialTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }
            var reponseDesign = _unitOfWork.MaterialRepository.Get(
                filter,
                orderBy,
                /*includeProperties: "",*/
                pageIndex: requestSearchMaterialModel.pageIndex,
                pageSize: requestSearchMaterialModel.pageSize, m => m.Designs
                ).Select(m => m.toMaterialDTO());
            return Ok(reponseDesign);
        }

        [HttpGet("{id}")]
        public IActionResult GetMaterialById(int id)
        {
            var Material = _unitOfWork.MaterialRepository.GetByID(id, m => m.Designs);

            if (Material == null)
            {
                return NotFound();
            }

            return Ok(Material.toMaterialDTO());
        }

        [HttpPost]
        public IActionResult CreateMaterial(RequestCreateMaterialModel requestCreateMaterialModel)
        {
            var Material = requestCreateMaterialModel.toMaterialEntity();
            _unitOfWork.MaterialRepository.Insert(Material);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }

        [HttpPut]
        public IActionResult UpdateMaterial(int id, RequestCreateMaterialModel requestCreateMaterialModel)
        {
            var existedMaterial = _unitOfWork.MaterialRepository.GetByID(id);
            if (existedMaterial == null)
            {
                return NotFound();
            }
            existedMaterial.Name = requestCreateMaterialModel.Name;
            existedMaterial.Price = requestCreateMaterialModel.Price;
            existedMaterial.ManagerId = requestCreateMaterialModel.ManagerId;
            _unitOfWork.MaterialRepository.Update(existedMaterial);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMaterial(int id)
        {
            var existedMaterial = _unitOfWork.MaterialRepository.GetByID(id);
            if (existedMaterial == null)
            {
                return NotFound();
            }
            _unitOfWork.MaterialRepository.Delete(existedMaterial);
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateException ex)
            {
                if (_unitOfWork.IsForeignKeyConstraintViolation(ex))
                {
                    return BadRequest("Cannot delete this item because it is referenced by another entity.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Delete Successfully");
        }

        
    }
}
