using System.ComponentModel.DataAnnotations;

namespace API.Model.MasterGemstoneModel
{
    public class RequestCreateMasterGemstoneModel
    {
        public int MasterGemstoneId { get; set; }
        [Required(ErrorMessage = "Kind là bắt buộc.")]
        [RegularExpression(@"^[^\d]", ErrorMessage = "Kind is string")]
        public string Kind { get; set; } = "string";
        [Range(0, double.MaxValue, ErrorMessage = "Size must be positive number")]
        public decimal Size { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive number")]
        public decimal Price { get; set; }
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Clarity is string")]
        public string Clarity { get; set; }
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Cut is string")]
        public string Cut { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Weight must be positive number")]
        public decimal Weight { get; set; }
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Shape is string")]
        public string Shape { get; set; }

        public string Image { get; set; }
    }
}
