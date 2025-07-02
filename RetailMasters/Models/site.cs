using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Site
{
    public int Siteid { get; set; }

    public string Sitecode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? Supplierid { get; set; }

    public string? Shiftstatus { get; set; }

    public bool? Isactive { get; set; }

    public string? Postcode { get; set; }

    public decimal? Shopcharge { get; set; }

    public decimal? Valetcharge { get; set; }

    public decimal? Workcharge { get; set; }

    public decimal? Marketingservices { get; set; }

    public decimal? Insurance { get; set; }

    public decimal? Subwayfacility { get; set; }

    public decimal? Costafacility { get; set; }

    public decimal? Substopfacility { get; set; }

    public decimal? Vwcountryfacility { get; set; }

    public decimal? Penceperlitre { get; set; }

    public int? Creditterms { get; set; }

    public string? Agreementno { get; set; }

    public string? Department { get; set; }

    public string? Customerref { get; set; }

    public bool? Hideinpricechange { get; set; }

    public bool? Disableonlinelottery { get; set; }

    public bool? Disableinstantlottery { get; set; }

    public bool? Disablepayzone { get; set; }

    public bool? Disableonlinelotterypaidout { get; set; }

    public bool? Disablepaypoint { get; set; }

    public bool? Disableepay { get; set; }

    public bool? Disablemotsales { get; set; }

    public bool? Disablecompanyonlinelottery { get; set; }

    public bool? Disableatm { get; set; }

    public bool? Disableshopsales { get; set; }

    public bool? Disablepaypointpayout { get; set; }

    public bool? Disablecompanygoodspayout { get; set; }

    public bool? Disablecompanynmp { get; set; }

    public virtual ICollection<Fuelprice> Fuelprices { get; set; } = new List<Fuelprice>();

    public virtual ICollection<Pump> Pumps { get; set; } = new List<Pump>();

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();

    public virtual ICollection<Valetprice> Valetprices { get; set; } = new List<Valetprice>();
}
