using Microsoft.AspNetCore.Mvc;
using API.Model.MasterGemstoneModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repositories;
using Repositories.Entity;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterGemstoneController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MasterGemstoneController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult SearchMasterGemstone([FromQuery] RequestSearchMasterGemstoneModel requestSearchMasterGemstoneModel)
        {
            var sortBy = requestSearchMasterGemstoneModel.SortContent != null ? requestSearchMasterGemstoneModel.SortContent?.sortMasterGemstoneBy.ToString() : null;
            var sortType = requestSearchMasterGemstoneModel.SortContent != null ? requestSearchMasterGemstoneModel.SortContent?.sortMasterGemstoneType.ToString() : null;
            Expression<Func<MasterGemstone, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchMasterGemstoneModel.Kind) || x.Kind.Contains(requestSearchMasterGemstoneModel.Kind)) &&
                (x.Size == requestSearchMasterGemstoneModel.Size || requestSearchMasterGemstoneModel.Size == null) &&
                (string.IsNullOrEmpty(requestSearchMasterGemstoneModel.Clarity) || x.Clarity.Contains(requestSearchMasterGemstoneModel.Clarity)) &&
                (string.IsNullOrEmpty(requestSearchMasterGemstoneModel.Cut) || x.Cut.Contains(requestSearchMasterGemstoneModel.Cut)) &&
                (string.IsNullOrEmpty(requestSearchMasterGemstoneModel.Shape) || x.Shape.Contains(requestSearchMasterGemstoneModel.Shape)) &&
                x.Price >= requestSearchMasterGemstoneModel.FromPrice &&
                (x.Price <= requestSearchMasterGemstoneModel.ToPrice|| requestSearchMasterGemstoneModel.ToPrice == null)&&
                x.Weight >= requestSearchMasterGemstoneModel.FromWeight &&
                (x.Weight <= requestSearchMasterGemstoneModel.ToWeight || requestSearchMasterGemstoneModel.ToWeight == null);

            Func<IQueryable<MasterGemstone>, IOrderedQueryable<MasterGemstone>> orderBy = null;


            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortMasterGemstoneTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortMasterGemstoneTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }



            var reponseGemStone = _unitOfWork.MasterGemstoneRepository.Get(
                filter,
                orderBy,
                pageIndex: requestSearchMasterGemstoneModel.pageIndex,
                pageSize: requestSearchMasterGemstoneModel.pageSize,
                x => x.Designs
                ).Select(x => x.toMasterGemstonesDTO());
            return Ok(reponseGemStone);
        }

        [HttpGet("{id}")]
        public IActionResult GetMasterGemstoneById(int id)
        {
            var MasterGemstone = _unitOfWork.MasterGemstoneRepository.GetByID(id, p => p.Designs);
            if (MasterGemstone == null)
            {
                return NotFound("MasterGemstone is not existed");
            }
            return Ok(MasterGemstone.toMasterGemstonesDTO());
        }

        [HttpPost]
        public IActionResult CreateMasterGemstone(RequestCreateMasterGemstoneModel requestCreateMasterGemstone)
        {
            var error = "";
            var properties = typeof(RequestCreateMasterGemstoneModel).GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(decimal))
                {
                    var value = property.GetValue(requestCreateMasterGemstone);
                    if ((decimal)value < 0)
                    {
                        error = property.Name + " must be positive number";
                        break;
                    }
                }
                if (property.PropertyType == typeof(string))
                {
                    var value = property.GetValue(requestCreateMasterGemstone);
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
            Expression<Func<MasterGemstone, bool>> filter = x =>
                (x.Kind.Equals(requestCreateMasterGemstone.Kind)) &&
                (x.Size == requestCreateMasterGemstone.Size) &&
                (x.Clarity.Equals(requestCreateMasterGemstone.Clarity)) &&
                (x.Cut.Equals(requestCreateMasterGemstone.Cut)) &&
                (x.Shape.Equals(requestCreateMasterGemstone.Shape)) &&
                x.Price == requestCreateMasterGemstone.Price &&
                x.Weight == requestCreateMasterGemstone.Weight;
            var existedMasterGemstone = _unitOfWork.MasterGemstoneRepository.Get(filter);
            if (existedMasterGemstone.Count() > 0)
            {
                return BadRequest("Master Gemstone is existed");
            }
            var MasterGemstone = requestCreateMasterGemstone.toMasterGemstonesEntity();
            _unitOfWork.MasterGemstoneRepository.Insert(MasterGemstone);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMasterGemstone([FromRoute] int id, RequestCreateMasterGemstoneModel requestCreateMasterGemstone)
        {
            var ExistedMasterGemstone = _unitOfWork.MasterGemstoneRepository.GetByID(id);
            if (ExistedMasterGemstone == null)
            {
                return NotFound("MasterGemstone is not existed");
            }

            Expression<Func<MasterGemstone, bool>> filter = x =>
                (x.Kind.Equals(requestCreateMasterGemstone.Kind)) &&
                (x.Size == requestCreateMasterGemstone.Size) &&
                (x.Clarity.Equals(requestCreateMasterGemstone.Clarity)) &&
                (x.Cut.Equals(requestCreateMasterGemstone.Cut)) &&
                (x.Shape.Equals(requestCreateMasterGemstone.Shape)) &&
                x.Price == requestCreateMasterGemstone.Price &&
                x.Weight == requestCreateMasterGemstone.Weight;
            var existedMasterGemstone = _unitOfWork.MasterGemstoneRepository.Get(filter);
            if (existedMasterGemstone.Count() > 0)
            {
                return BadRequest("Kind, Size, Clarity, Cut, Shape, Price, Weight value of Update Master Gemstone is same with old value");
            }
            ExistedMasterGemstone.Kind = requestCreateMasterGemstone.Kind;
            ExistedMasterGemstone.Size = requestCreateMasterGemstone.Size;
            ExistedMasterGemstone.Price = requestCreateMasterGemstone.Price;
            ExistedMasterGemstone.Clarity = requestCreateMasterGemstone.Clarity;
            ExistedMasterGemstone.Cut = requestCreateMasterGemstone.Cut;
            ExistedMasterGemstone.Weight = requestCreateMasterGemstone.Weight;
            ExistedMasterGemstone.Shape = requestCreateMasterGemstone.Shape;
            _unitOfWork.MasterGemstoneRepository.Update(ExistedMasterGemstone);
            _unitOfWork.Save();
            return Ok("Update Master Gemstone successfully");
        }

        [HttpDelete]
        public IActionResult DeleteMasterGemstone(int id)
        {
            var ExistedMasterGemstone = _unitOfWork.MasterGemstoneRepository.GetByID(id);
            if (ExistedMasterGemstone == null)
            {
                return NotFound("MasterGemstone is not existed");
            }
            _unitOfWork.MasterGemstoneRepository.Delete(ExistedMasterGemstone);
            try
            {
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

            return Ok("Delete Successfully");
        }

    }
}
