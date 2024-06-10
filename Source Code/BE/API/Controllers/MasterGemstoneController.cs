using Microsoft.AspNetCore.Mvc;
using API.Model.MasterGemstoneModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repositories;

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
        public IActionResult SearchBlog([FromQuery] RequestSearchMasterGemstoneModel requestSearchMasterGemstoneModel)
        {
            var sortBy = requestSearchMasterGemstoneModel.SortContent != null ? requestSearchMasterGemstoneModel.SortContent?.sortMasterGemstoneBy.ToString() : null;
            var sortType = requestSearchMasterGemstoneModel.SortContent != null ? requestSearchMasterGemstoneModel.SortContent?.sortMasterGemstoneType.ToString() : null;
            var groupBy = requestSearchMasterGemstoneModel.SortContent != null ? requestSearchMasterGemstoneModel.SortContent?.groupBy.ToString() : null ;
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

            if (!string.IsNullOrEmpty(groupBy))
            {
                orderBy = query => (IOrderedQueryable<MasterGemstone>)query.GroupBy(p => EF.Property<object>(p, sortBy));
            }

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
                return NotFound();
            }
            return Ok(MasterGemstone.toMasterGemstonesDTO());
        }

        [HttpPost]
        public IActionResult CreateMasterGemstone(RequestCreateMasterGemstoneModel requestCreateMasterGemstone)
        {
            var MasterGemstone = requestCreateMasterGemstone.toMasterGemstonesEntity();
            _unitOfWork.MasterGemstoneRepository.Insert(MasterGemstone);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMasterGemstone([FromRoute] int id, RequestCreateMasterGemstoneModel requestCreateMasterGemstone)
        {
            var ExistedMasterGemstone = _unitOfWork.MasterGemstoneRepository.GetByID(id);
            if (ExistedMasterGemstone == null)
            {
                return NotFound();
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
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMasterGemstones(int id)
        {
            var ExistedMasterGemstone = _unitOfWork.MasterGemstoneRepository.GetByID(id);
            if (ExistedMasterGemstone == null)
            {
                return NotFound();
            }
            _unitOfWork.MasterGemstoneRepository.Delete(ExistedMasterGemstone);
            _unitOfWork.Save();
            return Ok();
        }

    }
}
