using API.Model.UserModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repositories;
using Repositories.Entity;
using SWP391Project.Repositories.Token;
using SWP391Project.Services.LoginGoogleSystem;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginGoogleController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly GoogleApiHelper _googleApiHelper;
        private readonly IToken _tokenService;
        private readonly string clientId = "528761239720-ac1sb7qru7cnvmmbddpsi8plsgsqrrg4.apps.googleusercontent.com";
        private readonly string clientSecret = "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";
        private readonly string redirectUri = "https://fpt-jewelry-production.netlify.app/login";
        public LoginGoogleController(UnitOfWork unitOfWork, IToken tokenService)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _googleApiHelper = new GoogleApiHelper();
        }   

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenRequest model ) {
            try
            {
                await Console.Out.WriteLineAsync(model.token);
                Console.WriteLine("Received Code: " + model.token);
                var userInfo = await _googleApiHelper.GetUserInfoAsync(clientId, clientSecret, model.token, redirectUri);
             if(userInfo != null) {
                    /*Console.WriteLine(userInfo);*/
                    Expression<Func<Users, bool>> filter = x =>
                   (x.Email.Equals(userInfo.Email));
                    var user = _unitOfWork.UserRepository.Get(filter: x=>x.Email==userInfo.Email, includes: m=> m.Role).FirstOrDefault();
                    if (user == null) {
                        userInfo.RoleId = 6;
                        var roleEntity = _unitOfWork.RoleRepository.GetByID(6);
                        userInfo.Password = userInfo.Email+"#";
                        var userEntity = userInfo.FromUserInfoGoogleToUserEntity(roleEntity);
                        _unitOfWork.UserRepository.Insert(userEntity);
                        _unitOfWork.Save();
                        return Ok(await _tokenService.CreateToken(userEntity));
                    }
                    else
                    {
                        return Ok(await _tokenService.CreateToken(user));
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
    }
}
