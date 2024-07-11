using API.Model.MasterGemstoneModel;
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
        [HttpGet("GetTotalRecords")]
        public IActionResult GetTotalRecords([FromQuery] RequestSearchStonesModel requestSearchStonesModel)
        {
            Expression<Func<Stones, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchStonesModel.Kind) || x.Kind.Contains(requestSearchStonesModel.Kind)) &&
                (x.Size == requestSearchStonesModel.Size || requestSearchStonesModel.Size == null) &&
                x.Quantity >= requestSearchStonesModel.FromQuantity &&
                (x.Quantity <= requestSearchStonesModel.ToQuantity || requestSearchStonesModel.ToQuantity == null) &&
                x.Price >= requestSearchStonesModel.FromPrice &&
                (x.Price <= requestSearchStonesModel.ToPrice || requestSearchStonesModel.ToPrice == null);

            var totalRecords = _unitOfWork.StoneRepository.Count(filter);

            var response = new
            {
                TotalRecords = totalRecords
            };

            return Ok(response);
        }
        [HttpGet]
        public IActionResult SearchStones([FromQuery] RequestSearchStonesModel requestSearchStonesModel)
        {
            try
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
                    x => x.Designs
                    ).Select(x => x.toStonesDTO());
                return Ok(reponseDesign);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in SearchStones");
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetStonesById(int id)
        {
            try
            {
                var Stones = _unitOfWork.StoneRepository.GetByID(id, p => p.Designs);
                if (Stones == null)
                {
                    return NotFound("Stones is not existed");
                }
                return Ok(Stones.toStonesDTO());
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetStonesById");
            }
           
        }
        [HttpPost]
        public IActionResult CreateStones(RequestCreateStonesModel requestCreateStonesModel)
        {
            var error = "";
            var properties = typeof(RequestCreateStonesModel).GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(decimal))
                {
                    var value = property.GetValue(requestCreateStonesModel);
                    if ((decimal)value < 0)
                    {
                        error = property.Name + " must be positive number";
                        break;
                    }
                }
                if (property.PropertyType == typeof(string))
                {
                    var value = property.GetValue(requestCreateStonesModel);
                    if (string.IsNullOrEmpty((string)value))
                    {
                        error = property.Name + " must be not plank";
                        break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            var getStoneSize = _unitOfWork.StoneRepository.Get(filter: x => (x.Kind == requestCreateStonesModel.Kind) && (x.Size == requestCreateStonesModel.Size) && (x.Quantity == requestCreateStonesModel.Quantity)).FirstOrDefault();
            if (getStoneSize != null)
            {
                return BadRequest("Stone with this size and quantity had existed");
            }
            try
            {
                var stones = requestCreateStonesModel.toStonesEntity();
                _unitOfWork.StoneRepository.Insert(stones);
                _unitOfWork.Save();
                return Ok("Create successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Create failed");
            }
        }
        [HttpPut]
        public IActionResult UpdateStones(int id, RequestCreateStonesModel requestCreateStonesModel)
        {
            try
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
                return Ok("Update Stones successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Update Stones failed");
            }
            
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
