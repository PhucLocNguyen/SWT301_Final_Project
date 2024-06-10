using System.ComponentModel.DataAnnotations;

namespace API.Model.StonesModel
{
    public class RequestCreateStonesModel
    {
        public string Kind { get; set; }
        public decimal Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
