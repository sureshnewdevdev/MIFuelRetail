using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Nonmeteredsalestype
{
    public int Salestypeid { get; set; }

    public string Typename { get; set; } = null!;

    public string? Description { get; set; }
}
