using API.Model.TypeOfJewellryModel;
using API.Model.UserModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using Repositories.Token;
using System.Linq.Expressions;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IToken _tokenService;
        public UserController(UnitOfWork unitOfWork, IToken tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        [HttpPost("registerForCustomer")]
        public async Task<IActionResult> Register([FromBody] RequestRegisterAccount requestRegisterAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (checkDuplicateUsername(requestRegisterAccount.Username))
                {
                    return BadRequest("Username is existed");
                }
                if (!requestRegisterAccount.Password.Equals(requestRegisterAccount.PasswordConfirm))
                {
                    return BadRequest("PasswordConfirm is not correct");
                }
                var roleEntity = _unitOfWork.RoleRepository.Get(filter: x => x.Name.Equals(RoleConst.Customer)).FirstOrDefault();
                var registerAccount = requestRegisterAccount.toUserEntity(roleEntity);
                _unitOfWork.UserRepository.Insert(registerAccount);
                _unitOfWork.Save();
                /*return Ok(registerAccount.toUserDTO());*/
                return Ok("Regist Successfully");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    return Conflict("Email has already been registered");
                }
                else
                {
                    return Problem("Something appear when registing", statusCode: 500);
                }
            }
            
        }

        [HttpPost("registerForAdmin")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Customer)]
        public  IActionResult RegisterForAdmin([FromBody] RequestRegisterAccount requestRegisterAccount, [FromQuery] RoleEnum roleEnum)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (checkDuplicateUsername(requestRegisterAccount.Username))
                {
                    return BadRequest("Username is existed");
                }
                if (!requestRegisterAccount.Password.Equals(requestRegisterAccount.PasswordConfirm))
                {
                    return BadRequest("PasswordConfirm is not correct");
                }
                var role = roleEnum.ToString() != "0" ? roleEnum.ToString() : "Customer";
                var roleEntity = _unitOfWork.RoleRepository.Get(filter: x => x.Name.Equals(role)).FirstOrDefault();
                var registerAccount = requestRegisterAccount.toUserEntity(roleEntity);
                _unitOfWork.UserRepository.Insert(registerAccount);
                _unitOfWork.Save();
                //return Ok(registerAccount.toUserDTO());
                return Ok("Regist Successfully");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    return BadRequest("Email has already been registered");
                }
                else
                {
                    return Problem("Something appear when registing", statusCode: 500);
                }
            }
           
        }
        [HttpPost("loginForCustomer")]
        public  async Task<IActionResult> LoginForCustomer(RequestLoginAccount loginDTO)
        {
            try
            {
                Expression<Func<Users, bool>> filter = x =>
                    (x.Username.Equals(loginDTO.Username));
                var user = _unitOfWork.UserRepository.Get(
                    includes: m => m.Role
                    ).Where(x=> x.Username.Equals(loginDTO.Username, StringComparison.Ordinal)).FirstOrDefault();

                if (user == null) { return BadRequest("Invalid Username"); }

                if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password)) { return BadRequest("Password incorrect"); }

                if(user.Role.Name == RoleConst.Customer)
                {
                    return Ok(await _tokenService.CreateToken(user));
                }
                return BadRequest("You do not have permission to access this page");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("The account does not register");
            }
        }

        [HttpPost("loginForStaff")]
        public async Task<IActionResult> LoginForStaff(RequestLoginAccount loginDTO)
        {
            try
            {
                Expression<Func<Users, bool>> filter = x =>
                    (x.Username.Equals(loginDTO.Username));
                var user = _unitOfWork.UserRepository.Get(
                    includes: m => m.Role
                    ).Where(x => x.Username.Equals(loginDTO.Username, StringComparison.Ordinal)).FirstOrDefault();

                if (user == null) { return BadRequest("Invalid Username"); }

                if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password)) { return BadRequest("Password incorrect"); }


                if (user.Role.Name != RoleConst.Customer)
                {
                    return Ok(await _tokenService.CreateToken(user));
                }
                return BadRequest("You do not have permission to access this page");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("The account does not register");
            }
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string RoleFromInput = null)
        {
            Expression<Func<Users, bool>> filter = x =>
                (string.IsNullOrEmpty(RoleFromInput) || x.Role.Name.Contains(RoleFromInput));
            var Users = _unitOfWork.UserRepository.Get(filter);
            return Ok(Users);
        }

        [HttpGet("{username}")]
        public IActionResult GetByUsername([FromRoute]string username)
        {
            Expression<Func<Users, bool>> filter = x =>
                (string.IsNullOrEmpty(username) || x.Username.Equals(username));
            var Users = _unitOfWork.UserRepository.Get(filter);
            return Ok(Users);
        }

        [HttpPut]
        public IActionResult UpdateProfile(int userId, UserDTO editUser)
        {
            try
            {
                var existedUser = _unitOfWork.UserRepository.GetByID(userId);
                if (existedUser == null)
                {
                    return NotFound("User does not found");
                }
                existedUser.Name = editUser.Name;
                existedUser.Phone = editUser.Phone;
                existedUser.Image = editUser.Image;
                _unitOfWork.UserRepository.Update(existedUser);
                _unitOfWork.Save();
                return Ok("Edit profile succesfully");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    return Conflict("Email has already been registered");
                }
                else
                {
                    return Problem("Something appear when registing", statusCode: 500);
                }
            }
        }

        private bool checkDuplicateUsername(string username)
        {
            bool check = false;
            var existedAccount = _unitOfWork.UserRepository.Get();
            foreach (var item in existedAccount)
            {
                if (item.Username.Equals(username)) 
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
    }
}
