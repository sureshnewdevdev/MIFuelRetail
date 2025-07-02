using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Valetprice
{
    public int Valetpriceid { get; set; }

    public int Siteid { get; set; }

    public int Valettypeid { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? Effectivedate { get; set; }

    public virtual Site Site { get; set; } = null!;

    public virtual Valettype Valettype { get; set; } = null!;
}
