using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Have
{
    public int WarrantyCardId { get; set; }

    public int RequirementId { get; set; }

    public DateOnly? DateCreated { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public virtual Requirement Requirement { get; set; } = null!;

    public virtual WarrantyCard WarrantyCard { get; set; } = null!;
}
