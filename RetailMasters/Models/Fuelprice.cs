using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Fuelprice
{
    public int Fuelpriceid { get; set; }

    public int Siteid { get; set; }

    public int Fueltypeid { get; set; }

    public decimal? Price { get; set; }

    public DateOnly Effectivedate { get; set; }

    public virtual Fueltype Fueltype { get; set; } = null!;

    public virtual Site Site { get; set; } = null!;
}
