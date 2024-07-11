using Repositories.Entity;

namespace API.Model.WarrantyCardModel
{
    public static class WarrantyCardMapper
    {
        public static WarrantyCard ToWarrantyCardEntity(this RequestWarrantyCardModel requestWarrantyCardModel)
        {
            return new WarrantyCard
            {
                Title = requestWarrantyCardModel.Title,
                Description = requestWarrantyCardModel.Description
            };
        }

        public static WarrantyCardDTO ToWarrantyCardDTO(this WarrantyCard warrantyCard)
        {
            return new WarrantyCardDTO
            {
                WarrantyCardId = warrantyCard.WarrantyCardId,
                Title = warrantyCard.Title,
                Description = warrantyCard.Description
            };
        }
    }
}
