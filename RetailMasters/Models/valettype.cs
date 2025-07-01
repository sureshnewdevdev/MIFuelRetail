using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class valettype
{
    public int valettypeid { get; set; }

    public string valettypename { get; set; } = null!;

    public virtual ICollection<valetprice> valetprices { get; set; } = new List<valetprice>();
}
