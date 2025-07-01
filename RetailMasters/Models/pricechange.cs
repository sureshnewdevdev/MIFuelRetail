using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class pricechange
{
    public int changeid { get; set; }

    public int fueltypeid { get; set; }

    public decimal? oldprice { get; set; }

    public decimal? newprice { get; set; }

    public DateTime? changedate { get; set; }

    public virtual fueltype fueltype { get; set; } = null!;
}
