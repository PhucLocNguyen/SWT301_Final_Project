using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class MasterGemstone
{
    public int MasterGemstoneId { get; set; }

    public string Kind { get; set; } = null!;

    public decimal Size { get; set; }

    public decimal Price { get; set; }

    public string Clarity { get; set; } = null!;

    public string Cut { get; set; } = null!;

    public decimal Weight { get; set; }

    public string Shape { get; set; } = null!;

    public string Image { get; set; } = null!;

    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();
}
