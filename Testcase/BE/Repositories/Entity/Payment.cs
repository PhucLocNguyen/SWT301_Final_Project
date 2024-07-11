using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = null!;

    public DateTime? CompletedAt { get; set; }

    public string Status { get; set; } = null!;

    public string? Content { get; set; }

    public int? CustomerId { get; set; }

    public int? RequirementsId { get; set; }

    public virtual Users? Customer { get; set; }

    public virtual Requirement? Requirements { get; set; }
}
