using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Creditcard
{
    public int Cardid { get; set; }

    public string? Cardname { get; set; }

    public string? Cardtype { get; set; }
}
