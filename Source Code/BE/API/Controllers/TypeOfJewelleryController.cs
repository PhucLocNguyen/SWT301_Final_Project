using API.Model.TypeOfJewellryModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfJewelleryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TypeOfJewelleryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] RequestSearchTypeOfJewelleryModel requestSearchTypeOfJewelleryModel)
        {

            var sortBy = requestSearchTypeOfJewelleryModel.SortContent != null ? requestSearchTypeOfJewelleryModel.SortContent?.sortTypeOfJewelleryBy.ToString() : null;
            var sortType = requestSearchTypeOfJewelleryModel.SortContent != null ? requestSearchTypeOfJewelleryModel.SortContent?.sortTypeOfJewelleryType.ToString() : null;
            Expression<Func<TypeOfJewellery, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchTypeOfJewelleryModel.Name) || x.Name.Contains(requestSearchTypeOfJewelleryModel.Name));
            Func<IQueryable<TypeOfJewellery>, IOrderedQueryable<TypeOfJewellery>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortTypeOfJewelleryTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortTypeOfJewelleryTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }
            var reponseJewellery = _unitOfWork.TypeOfJewellryRepository.Get(filter,
                orderBy,
                pageIndex: requestSearchTypeOfJewelleryModel.pageIndex,
                pageSize: requestSearchTypeOfJewelleryModel.pageSize,
                includes: m =>m.Designs).Select(x=>x.toTypeOfJewelleryDTO());
            return Ok(reponseJewellery);
        }

        [HttpGet("{id}")]
        public IActionResult GetTypeOfJewelleryById(int id)
        {
            var TypeOfJewellery = _unitOfWork.TypeOfJewellryRepository.GetByID(id,p=>p.Designs);
            if (TypeOfJewellery == null)
            {
                return NotFound();
            }

            return Ok(TypeOfJewellery.toTypeOfJewelleryDTO());
        }

        [HttpPost]
        public IActionResult CreateTypeOfJewellery(RequestCreateTypeOfJewelleryModel requestTypeOfJewelleryModel)
        {
            var TypeOfJewellery = new TypeOfJewellery()
            {
                Name = requestTypeOfJewelleryModel.Name,
            };
            _unitOfWork.TypeOfJewellryRepository.Insert(TypeOfJewellery);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateTypeOfJewellery(int id, RequestCreateTypeOfJewelleryModel requestTypeOfJewelleryModel)
        {
            var existedTypeOfJewellery = _unitOfWork.TypeOfJewellryRepository.GetByID(id);
            if (existedTypeOfJewellery == null)
            {
                return NotFound();
            }
            existedTypeOfJewellery.Name = requestTypeOfJewelleryModel.Name;
            _unitOfWork.TypeOfJewellryRepository.Update(existedTypeOfJewellery);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteTypeOfJewellery(int id)
        {
            var existedTypeOfJewellery = _unitOfWork.TypeOfJewellryRepository.GetByID(id);
            if (existedTypeOfJewellery == null)
            {
                return NotFound();
            }
            _unitOfWork.TypeOfJewellryRepository.Delete(existedTypeOfJewellery);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
