using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Material
{
    public int MaterialId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Image { get; set; } = null!;

    public int ManagerId { get; set; }

    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

    public virtual Users Manager { get; set; } = null!;
}
