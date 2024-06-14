using API.Model.PaymentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult SearchPayment([FromQuery] RequestSearchPaymentModel requestSearchPaymentModel)
        {
            var sortBy = requestSearchPaymentModel.SortContent != null ? requestSearchPaymentModel.SortContent?.sortPaymentBy.ToString() : null;
            var sortType = requestSearchPaymentModel.SortContent != null ? requestSearchPaymentModel.SortContent?.sortPaymentType.ToString() : null;
            Expression<Func<Payment, bool>> filter = x =>
                (string.IsNullOrEmpty(requestSearchPaymentModel.Method) || x.Method.Contains(requestSearchPaymentModel.Method)) &&
                (x.CustomerId == requestSearchPaymentModel.CustomerId || requestSearchPaymentModel.CustomerId == null) &&
                (x.RequirementsId == requestSearchPaymentModel.RequirementsId || requestSearchPaymentModel.RequirementsId == null) &&
                x.Amount >= requestSearchPaymentModel.FromAmount &&
                (x.Amount <= requestSearchPaymentModel.ToAmount || requestSearchPaymentModel.ToAmount == null);
            Func<IQueryable<Payment>, IOrderedQueryable<Payment>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortPaymentTypeEnum.Ascending.ToString())
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortType == SortPaymentTypeEnum.Descending.ToString())
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }
            var reponseDesign = _unitOfWork.PaymentRepository.Get(
                filter,
                orderBy,
                includeProperties: "",
                pageIndex: requestSearchPaymentModel.pageIndex,
                pageSize: requestSearchPaymentModel.pageSize
                );
            return Ok(reponseDesign);
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var Payment = _unitOfWork.PaymentRepository.GetByID(id);

            if (Payment == null)
            {
                return NotFound();
            }

            return Ok(Payment);
        }

        [HttpPost]
        public IActionResult CreatePayment(RequestCreatePaymentModel requestCreatePaymentModel)
        {
            var Payment = requestCreatePaymentModel.ToPaymentEntity();
            _unitOfWork.PaymentRepository.Insert(Payment);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, RequestCreatePaymentModel requestCreatePaymentModel)
        {
            var ExistPayment = _unitOfWork.PaymentRepository.GetByID(id);

            if (ExistPayment == null)
            {
                return NotFound();
            }

            ExistPayment.Amount = (decimal)requestCreatePaymentModel.Amount;
            ExistPayment.Method = requestCreatePaymentModel.Method;
            ExistPayment.CustomerId = requestCreatePaymentModel.CustomerId;
            ExistPayment.RequirementsId = requestCreatePaymentModel.RequirementsId;

            _unitOfWork.PaymentRepository.Update(ExistPayment);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePayment(int id)
        {
            var ExistPayment = _unitOfWork.PaymentRepository.GetByID(id);

            if (ExistPayment == null)
            {
                return NotFound();
            }

            _unitOfWork.PaymentRepository.Delete(ExistPayment);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
