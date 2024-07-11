using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class DesignRule
{
    public int DesignRuleId { get; set; }

    public decimal? MinSizeMasterGemstone { get; set; }

    public decimal? MaxSizeMasterGemstone { get; set; }

    public decimal? MinSizeJewellery { get; set; }

    public decimal? MaxSizeJewellery { get; set; }

    public int TypeOfJewelleryId { get; set; }

    public virtual TypeOfJewellery TypeOfJewellery { get; set; } = null!;
}
