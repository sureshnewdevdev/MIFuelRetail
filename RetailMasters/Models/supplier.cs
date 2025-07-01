using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class supplier
{
    public int supplierid { get; set; }

    public string supdesc { get; set; } = null!;

    public virtual ICollection<site> sites { get; set; } = new List<site>();
}
