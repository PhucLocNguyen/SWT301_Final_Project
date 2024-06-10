namespace Repositories
{

    public partial class MasterGemstone
    {
        public int MasterGemstoneId { get; set; }

        public string Kind { get; set; } = null!;

        public decimal? Size { get; set; }

        public decimal? Price { get; set; }

        public string? Clarity { get; set; }

        public string? Cut { get; set; }

        public decimal? Weight { get; set; }

        public string? Shape { get; set; }

        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();
    }

}
