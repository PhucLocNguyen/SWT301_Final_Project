using System;
using System.Collections.Generic;

namespace Repositories.Entity;
public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int? ManagerId { get; set; }

    public virtual Users? Manager { get; set; }
}
