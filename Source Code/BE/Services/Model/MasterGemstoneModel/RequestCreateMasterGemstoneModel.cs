using System.ComponentModel.DataAnnotations;

namespace API.Model.MasterGemstoneModel
{
    public class RequestCreateMasterGemstoneModel
    {
        public int MasterGemstoneId { get; set; }
        public string Kind { get; set; } = "string";
        [Range(0, double.MaxValue, ErrorMessage = "Size must be positive number")]
        public decimal Size { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive number")]
        public decimal Price { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Weight must be positive number")]
        public decimal Weight { get; set; }
        public string Shape { get; set; }

        public string Image { get; set; }
    }
}
