using API.Model.VnPayModel;
using Repositories.Entity;

namespace API.Model.PaymentModel
{
    public static class PaymentMapper
    {
        public static Payment ToPaymentEntity(this RequestCreateVnpay vnpayDTO, int userId, int requirementId)
        {
            return new Payment
            {
                Amount = vnpayDTO.RequiredAmount,
                CompletedAt = vnpayDTO.PaymentDate,
                Content = vnpayDTO.PaymentContent,
                Method = "Vnpay",
                CustomerId = userId,
                RequirementsId = requirementId,
            };
        }
    }
}
