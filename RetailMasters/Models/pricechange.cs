using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Pricechange
{
    public int Changeid { get; set; }

    public int Fueltypeid { get; set; }

    public decimal? Oldprice { get; set; }

    public decimal? Newprice { get; set; }

    public DateTime? Changedate { get; set; }

    public virtual Fueltype Fueltype { get; set; } = null!;
}
