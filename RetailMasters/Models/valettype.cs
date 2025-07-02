using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Valettype
{
    public int Valettypeid { get; set; }

    public string Valettypename { get; set; } = null!;

    public virtual ICollection<Valetprice> Valetprices { get; set; } = new List<Valetprice>();
}
