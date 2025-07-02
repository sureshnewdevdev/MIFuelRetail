using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailMasters.Models;

public partial class Site
{
    [Column("siteid")]
    public int Siteid { get; set; }

    [Column("sitecode")]
    public string Sitecode { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("supplierid")]
    public int? Supplierid { get; set; }

    [Column("shiftstatus")]
    public string? Shiftstatus { get; set; }

    [Column("isactive")]
    public bool? Isactive { get; set; }

    [Column("postcode")]
    public string? Postcode { get; set; }

    [Column("shopcharge")]
    public decimal? Shopcharge { get; set; }

    [Column("valetcharge")]
    public decimal? Valetcharge { get; set; }

    [Column("workcharge")]
    public decimal? Workcharge { get; set; }

    [Column("marketingservices")]
    public decimal? Marketingservices { get; set; }

    [Column("insurance")]
    public decimal? Insurance { get; set; }

    [Column("subwayfacility")]
    public decimal? Subwayfacility { get; set; }

    [Column("costafacility")]
    public decimal? Costafacility { get; set; }

    [Column("substopfacility")]
    public decimal? Substopfacility { get; set; }

    [Column("vwcountryfacility")]
    public decimal? Vwcountryfacility { get; set; }

    [Column("penceperlitre")]
    public decimal? Penceperlitre { get; set; }

    [Column("creditterms")]
    public int? Creditterms { get; set; }

    [Column("agreementno")]
    public string? Agreementno { get; set; }

    [Column("department")]
    public string? Department { get; set; }

    [Column("customerref")]
    public string? Customerref { get; set; }

    [Column("hideinpricechange")]
    public bool? Hideinpricechange { get; set; }

    [Column("disableonlinelottery")]
    public bool? Disableonlinelottery { get; set; }

    [Column("disableinstantlottery")]
    public bool? Disableinstantlottery { get; set; }

    [Column("disablepayzone")]
    public bool? Disablepayzone { get; set; }

    [Column("disableonlinelotterypaidout")]
    public bool? Disableonlinelotterypaidout { get; set; }

    [Column("disablepaypoint")]
    public bool? Disablepaypoint { get; set; }

    [Column("disableepay")]
    public bool? Disableepay { get; set; }

    [Column("disablemotsales")]
    public bool? Disablemotsales { get; set; }

    [Column("disablecompanyonlinelottery")]
    public bool? Disablecompanyonlinelottery { get; set; }

    [Column("disableatm")]
    public bool? Disableatm { get; set; }

    [Column("disableshopsales")]
    public bool? Disableshopsales { get; set; }

    [Column("disablepaypointpayout")]
    public bool? Disablepaypointpayout { get; set; }

    [Column("disablecompanygoodspayout")]
    public bool? Disablecompanygoodspayout { get; set; }

    [Column("disablecompanynmp")]
    public bool? Disablecompanynmp { get; set; }

    [ForeignKey("Supplierid")]
    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Fuelprice> Fuelprices { get; set; } = new List<Fuelprice>();
    public virtual ICollection<Pump> Pumps { get; set; } = new List<Pump>();
    public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();
    public virtual ICollection<Valetprice> Valetprices { get; set; } = new List<Valetprice>();
}
