using API.Model.StonesModel;
using API.Model.UserModel;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Email;
using Repositories.Entity;
using SWP391Project.Repositories.Token;
using SWP391Project.Services.Model.UserModel;
using System.Linq.Expressions;
using static Repositories.Email.EmailService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IToken _tokenService;
        private readonly IEmailService _emailService;
        public UserController(UnitOfWork unitOfWork, IToken tokenService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        [HttpPost("registerForCustomer")]
        public async Task<IActionResult> Register([FromBody] RequestRegisterAccount requestRegisterAccount)
        {
            try
            {
                if (checkDuplicateUsername(requestRegisterAccount.Username))
                {
                    return BadRequest("Username is existed");
                }
                var existUserHaveSameEmail = _unitOfWork.UserRepository.Get(filter: x => x.Email.Equals(requestRegisterAccount.Email));
                if (existUserHaveSameEmail.Count() > 0)
                {
                    return BadRequest("Email has already been registered");
                }
                if (!requestRegisterAccount.Password.Equals(requestRegisterAccount.PasswordConfirm))
                {
                    return BadRequest("PasswordConfirm is not correct");
                }
                _emailService.SaveInCache(requestRegisterAccount);
                return Ok("Please check email to verify your email");
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong when sending email");
            }
            
        }

        [HttpPost("registerForAdmin")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Customer)]
        public IActionResult RegisterForAdmin([FromBody] RequestRegisterAccount requestRegisterAccount, [FromQuery] RoleEnum roleEnum)
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
        public async Task<IActionResult> LoginForCustomer(RequestLoginAccount loginDTO)
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

                if (user.Role.Name == RoleConst.Customer)
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
        public IActionResult GetAll([FromQuery] RequestSearchUserModel requestSearchUserModel)
        {
            Expression<Func<Users, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchUserModel.RoleFromInput) || x.Role.Name.Contains(requestSearchUserModel.RoleFromInput)) && x.Role.Name!=RoleConst.Customer && x.Role.Name != RoleConst.Admin;
            var Users = _unitOfWork.UserRepository.Get(
                filter, 
                pageIndex: requestSearchUserModel.pageIndex,
                pageSize: requestSearchUserModel.pageSize);
            return Ok(Users);
        }

        [HttpPost("GetUserId")]
        public IActionResult GetByUsername([FromBody]string userId)
        {
            
            var Users = _unitOfWork.UserRepository.GetByID(int.Parse(userId));
            return Ok(Users.toUserDTO());
        }

        [HttpGet]
        [Route("GetUserByRoleInRequirement")]
        public IActionResult GetUserByRoleInRequirement([FromQuery] RoleEnum RoleFromInput, [FromQuery] int requirementId)
        {

            var users = _unitOfWork.UserRequirementRepository
            .Get(filter: x => x.RequirementId == requirementId, includeProperties: "User")
            .Select(ur => ur.User).Where(x => x.RoleId == (int)RoleFromInput)
            .ToList();
            return Ok(users);
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

        [HttpPatch("ModifyAccount")]
        public IActionResult ModifyAccount(int userId, JsonPatchDocument ModifyAccount)
        {
            try
            {
                var operations = ModifyAccount.Operations;
                var existedUser = _unitOfWork.UserRepository.GetByID(userId);
                if (existedUser == null)
                {
                    return NotFound("User does not found");
                }
                foreach (var operation in operations)
                {
                    if (operation.path == "username")
                    {
                        var newValue = operation.value;

                        if (_unitOfWork.UserRepository.Get().Where(x => x.Username.Equals((string)newValue, StringComparison.Ordinal)).FirstOrDefault() != null)
                        {
                            return BadRequest("Username is existed");
                        }
                        //existedUser.Username = (string)newValue;
                    }
                    if (operation.path == "password")
                    {
                        var newValue = BCrypt.Net.BCrypt.HashPassword((string)operation.value);
                        operation.value = newValue;

                        //existedUser.Password = BCrypt.Net.BCrypt.HashPassword((string)newValue);
                    }
                }
                ModifyAccount.ApplyTo(existedUser);
                //_unitOfWork.UserRepository.Update(existedUser);
                _unitOfWork.Save();
                return Ok("Modify acount successfully");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Something appear when modify account");

            }
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> SendEmail([FromBody] string VerifyCodeFromUser)
        {

            try
            {
                var result = _emailService.VerifyCode(VerifyCodeFromUser);


                return result switch
                {
                    VerifyResult.Success => Ok("Regist Successfully"),
                    VerifyResult.Expired => BadRequest("Verification code has expired. Please sign up again"),
                    VerifyResult.Invalid => BadRequest("Invalid verification code. Please try again"),
                };

            }
            catch (Exception ex)
            {
                throw;
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
