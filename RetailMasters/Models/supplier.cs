using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Supplier
{
    public int Supplierid { get; set; }

    public string Supdesc { get; set; } = null!;

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();
}
