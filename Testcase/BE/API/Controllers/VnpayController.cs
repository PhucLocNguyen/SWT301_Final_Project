using API.Model.VnPayModel;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Entity;
using Repositories.VnPay.Services;
using SWP391Project.Services.Model.VnpayModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnpayController : ControllerBase
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VnpayService _vnpayService;
        private readonly UnitOfWork _unitOfWork;

        public VnpayController(/*IHttpContextAccessor httpContextAccessor,*/ VnpayService vnpayService, UnitOfWork unitOfWork)
        {
            /*_httpContextAccessor = httpContextAccessor;*/
            _vnpayService = vnpayService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public IActionResult CreatePayment([FromBody] RequestCreateVnpay requestCreateVnpay)
        {
            try
            {
                requestCreateVnpay.PaidAmount = requestCreateVnpay.RequiredAmount;
                //string? ipAddress = GetClientIpAddress(_httpContextAccessor.HttpContext);
                var result = _vnpayService.CreatePayment(requestCreateVnpay);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong when create Vnpay link");
            }
            
        }

        private string? GetClientIpAddress(HttpContext httpContext)
        {
            // Check for proxy headers
            string? ipAddress = httpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                // Use the IP address from the proxy header
                return ipAddress;
            }
            else
            {
                // Use the local IP address
                return httpContext?.Connection?.LocalIpAddress?.ToString();
            }
        }

        //Check payment response
        [HttpGet("CheckResponse")]
        public IActionResult CheckResponse()
        {
            try
            {
                ResponseMessage result = _vnpayService.checkPayment(Request.Query);
                var ResponseCode = result.ResponseCode;
                var PaymentStatusPending = _unitOfWork.PaymentRepository.Get(filter: x=>x.Status.Equals("Pending")&& x.PaymentId != result.Payment.PaymentId
                && x.CustomerId == result.Payment.CustomerId && x.RequirementsId == result.Payment.RequirementsId).Select(x=>x.PaymentId).ToList();
                if (ResponseCode.Equals("00"))
                {
                    result.Payment.Status = "Paid";
                    foreach (var item in PaymentStatusPending)
                    {
                        _unitOfWork.PaymentRepository.Delete(item);
                    }
                    _unitOfWork.Save();
                    return Ok(result.Payment.RequirementsId);
                }
                else
                {
                    result.Payment.Status = "Failed";
                    foreach (var item in PaymentStatusPending)
                    {
                        _unitOfWork.PaymentRepository.Delete(item);
                    }
                    _unitOfWork.Save();
                    return BadRequest(result.Payment.RequirementsId);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in CheckResponse");
            }
        }
    }
}
