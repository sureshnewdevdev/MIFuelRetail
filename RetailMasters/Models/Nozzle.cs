using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Nozzle
{
    public int Nozzleid { get; set; }

    public int Nozzlenumber { get; set; }

    public int Pumpid { get; set; }

    public int Tankid { get; set; }

    public virtual Pump Pump { get; set; } = null!;

    public virtual Tank Tank { get; set; } = null!;
}
