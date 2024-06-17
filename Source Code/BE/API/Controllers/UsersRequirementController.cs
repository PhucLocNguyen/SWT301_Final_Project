using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersRequirementController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UsersRequirementController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var UsersRequirement = _unitOfWork.UserRequirementRepository.Get();
            return Ok(UsersRequirement);
        }
    }
}
