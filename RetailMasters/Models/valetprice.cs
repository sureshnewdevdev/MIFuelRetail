using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class valetprice
{
    public int valetpriceid { get; set; }

    public int siteid { get; set; }

    public int valettypeid { get; set; }

    public decimal? price { get; set; }

    public DateOnly? effectivedate { get; set; }

    public virtual site site { get; set; } = null!;

    public virtual valettype valettype { get; set; } = null!;
}
