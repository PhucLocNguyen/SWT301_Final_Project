using API.Model.WarrantyCardModel;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyCardController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public WarrantyCardController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.WarrantyCardRepository.Get());
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetWarrantyCardById(int id)
        {
            var warrantyCard = _unitOfWork.WarrantyCardRepository
                        /*.GetByID(id, c => c.Haves.Select(h => h.Requirement));*/
                        .GetByID(id);

            if (warrantyCard == null)
            {
                return NotFound();
            }

            return Ok(warrantyCard);
        }

        [HttpPost]
        public IActionResult CreateWarrantyCard(RequestWarrantyCardModel requestWarrantyCardModel)
        {
            var WarrantyCard = requestWarrantyCardModel.ToWarrantyCardEntity();
            _unitOfWork.WarrantyCardRepository.Insert(WarrantyCard);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateWarrantyCard([FromRoute] int id, RequestWarrantyCardModel requestWarrantyCardModel)
        {
            var ExistWarrantyCard = _unitOfWork.WarrantyCardRepository.GetByID(id);

            if (ExistWarrantyCard == null)
            {
                return NotFound();
            }

            ExistWarrantyCard.Title = requestWarrantyCardModel.Title;
            ExistWarrantyCard.Description = requestWarrantyCardModel.Description;
            _unitOfWork.WarrantyCardRepository.Update(ExistWarrantyCard);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteWarrantyCard([FromRoute] int id)
        {
            var ExistWarrantyCard = _unitOfWork.WarrantyCardRepository.GetByID(id);

            if (ExistWarrantyCard == null)
            {
                return NotFound();
            }

            _unitOfWork.WarrantyCardRepository.Delete(ExistWarrantyCard);
            _unitOfWork.Save();
            return Ok();
        }

    }
}
