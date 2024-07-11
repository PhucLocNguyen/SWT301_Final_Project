using API.Model.WarrantyCardModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult SearchWarrantyCard()
        {
            try
            {
                return Ok(_unitOfWork.WarrantyCardRepository.Get());
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in SearchWarrantyCard");
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetWarrantyCardById(int id)
        {
            try
            {
                var warrantyCard = _unitOfWork.WarrantyCardRepository
                                        /*.GetByID(id, c => c.Haves.Select(h => h.Requirement));*/
                                        .GetByID(id);

                if (warrantyCard == null)
                {
                    return NotFound("WarrantyCard is not existed");
                }

                return Ok(warrantyCard);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetWarrantyCardById");
            }
            
        }

        [HttpPost]
        public IActionResult CreateWarrantyCard(RequestWarrantyCardModel requestWarrantyCardModel)
        {
            try
            {
                var WarrantyCard = requestWarrantyCardModel.ToWarrantyCardEntity();
                _unitOfWork.WarrantyCardRepository.Insert(WarrantyCard);
                _unitOfWork.Save();
                return Ok("Create Warranty successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Create Warranty failed");
            }
            
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateWarrantyCard([FromRoute] int id, RequestWarrantyCardModel requestWarrantyCardModel)
        {
            try
            {
                var ExistWarrantyCard = _unitOfWork.WarrantyCardRepository.GetByID(id);

                if (ExistWarrantyCard == null)
                {
                    return NotFound("WarrantyCard is not existed");
                }

                ExistWarrantyCard.Title = requestWarrantyCardModel.Title;
                ExistWarrantyCard.Description = requestWarrantyCardModel.Description;
                _unitOfWork.WarrantyCardRepository.Update(ExistWarrantyCard);
                _unitOfWork.Save();
                return Ok("Update Warranty successfully");
            } catch (Exception Ex)
            {
                return BadRequest("Update Warranty failed");
            }
            
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteWarrantyCard([FromRoute] int id)
        {
            var ExistWarrantyCard = _unitOfWork.WarrantyCardRepository.GetByID(id);

            if (ExistWarrantyCard == null)
            {
                return NotFound("WarrantyCard is not existed");
            }
            try
            {
                _unitOfWork.WarrantyCardRepository.Delete(ExistWarrantyCard);
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
