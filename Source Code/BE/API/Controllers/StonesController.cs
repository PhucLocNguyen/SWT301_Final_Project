using API.Model.StonesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StonesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public StonesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult SearchStones([FromQuery] RequestSearchStonesModel requestSearchStonesModel)
        {
            var sortBy = requestSearchStonesModel.SortContent != null ? requestSearchStonesModel.SortContent?.sortStonetBy.ToString() : null;
            var sortType = requestSearchStonesModel.SortContent != null ? requestSearchStonesModel.SortContent?.sortStonesType.ToString() : null;
            Expression<Func<Stones, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchStonesModel.Kind) || x.Kind.Contains(requestSearchStonesModel.Kind)) &&
                (x.Size == requestSearchStonesModel.Size || requestSearchStonesModel.Size == null) &&
                x.Quantity >= requestSearchStonesModel.FromQuantity &&
                (x.Quantity <= requestSearchStonesModel.ToQuantity || requestSearchStonesModel.ToQuantity == null) &&
                x.Price >= requestSearchStonesModel.FromPrice &&
                (x.Price <= requestSearchStonesModel.ToPrice || requestSearchStonesModel.ToPrice == null);
            Func<IQueryable<Stones>, IOrderedQueryable<Stones>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortStonesTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortStonesTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }
            var reponseDesign = _unitOfWork.StoneRepository.Get(
                filter,
                orderBy,
                pageIndex: requestSearchStonesModel.pageIndex,
                pageSize: requestSearchStonesModel.pageSize,
                x=>x.Designs
                ).Select(x=>x.toStonesDTO());
            return Ok(reponseDesign);
        }

        [HttpGet("{id}")]
        public IActionResult GetStonesById(int id)
        {
            var Stones = _unitOfWork.StoneRepository.GetByID(id,p=>p.Designs);
            if(Stones == null)
            {
                return NotFound("Stones is not existed");
            }
            return Ok(Stones.toStonesDTO());
        }
        [HttpPost]
        public IActionResult CreateStones(RequestCreateStonesModel requestCreateStonesModel)
        {
            var Stones = requestCreateStonesModel.toStonesEntity();
            _unitOfWork.StoneRepository.Insert(Stones);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }
        [HttpPut]
        public IActionResult UpdateStones(int id, RequestCreateStonesModel requestCreateStonesModel)
        {
            var existedStonesUpdate = _unitOfWork.StoneRepository.GetByID(id);
            if (existedStonesUpdate == null)
            {
                return NotFound("Stones is not existed");
            }
            existedStonesUpdate.Kind = requestCreateStonesModel.Kind;
            existedStonesUpdate.Price = requestCreateStonesModel.Price;
            existedStonesUpdate.Quantity = requestCreateStonesModel.Quantity;
            existedStonesUpdate.Size = requestCreateStonesModel.Size;
            _unitOfWork.StoneRepository.Update(existedStonesUpdate);
            _unitOfWork.Save();
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteStones(int id)
        {
            var existedStonesUpdate = _unitOfWork.StoneRepository.GetByID(id);
            if(existedStonesUpdate == null)
            { 
                return NotFound("Stones is not existed");
            }
            _unitOfWork.StoneRepository.Delete(existedStonesUpdate);
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
