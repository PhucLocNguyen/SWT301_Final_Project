using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Design
{
    public int DesignId { get; set; }

    public int? ParentId { get; set; }

    public string DesignName { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? Description { get; set; }

    public int? StonesId { get; set; }

    public int? MasterGemstoneId { get; set; }

    public int? ManagerId { get; set; }

    public int? MaterialId { get; set; }

    public int? TypeOfJewelleryId { get; set; }

    public virtual Users? Manager { get; set; }

    public virtual MasterGemstone? MasterGemstone { get; set; }

    public virtual Material? Material { get; set; }

    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();

    public virtual Stones? Stone { get; set; }

    public virtual TypeOfJewellery? TypeOfJewellery { get; set; }
}
