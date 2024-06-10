using API.Model.DesignModel;
using Repositories;

namespace API.Model.StonesModel
{
    public static class StonesMapper
    {
        public static Stones toStonesEntity(this RequestCreateStonesModel requestCreateStonesModel)
        {
            return new Stones()
            {
                Kind = requestCreateStonesModel.Kind,
                Price = requestCreateStonesModel.Price,
                Quantity = requestCreateStonesModel.Quantity,
                Size = requestCreateStonesModel.Size,
            };
        }

        public static StonesDTO toStonesDTO( this Stones stones)
        {
            return new StonesDTO()
            {
                StonesId = stones.StonesId,
                Kind = stones.Kind,
                Price = stones.Price,
                Quantity = stones.Quantity,
                Size = stones.Size,
                Designs = stones.Designs.Select(d => d.toCreateDesign()).ToList()
            };
        }

        public static RequestCreateStonesModel toCreateStones(this Stones stones)
        {
            return new RequestCreateStonesModel()
            {
                Kind = stones.Kind,
                Price = (decimal)stones.Price,
                Quantity = (int)stones.Quantity,
                Size = (decimal)stones.Size,
            };
        }
    }
}
