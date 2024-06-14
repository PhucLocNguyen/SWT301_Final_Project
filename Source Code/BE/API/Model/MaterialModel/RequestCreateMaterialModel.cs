namespace API.Model.MaterialModel
{
    public class RequestCreateMaterialModel
    {
        public int MaterialId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int ManagerId { get; set; }
    }
}
