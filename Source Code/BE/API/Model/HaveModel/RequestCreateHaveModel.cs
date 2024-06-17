namespace API.Model.HaveModel
{
    public class RequestCreateHaveModel
    {
        public int WarrantyCardId { get; set; }
        public int RequirementId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
