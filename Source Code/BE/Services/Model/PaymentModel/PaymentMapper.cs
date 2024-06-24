using Repositories.Entity;

namespace API.Model.PaymentModel
{
    public static class PaymentMapper
    {
        public static Payment ToPaymentEntity(this RequestCreatePaymentModel requestCreatePaymentModel)
        {
            return new Payment
            {
                Amount = (decimal)requestCreatePaymentModel.Amount,
                Method = requestCreatePaymentModel.Method,
                CustomerId = requestCreatePaymentModel.CustomerId,
                RequirementsId = requestCreatePaymentModel.RequirementsId,
            };
        }
    }
}
