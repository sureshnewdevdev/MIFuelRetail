using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RetailMasters.Models;


public partial class Supplier
{
    [Column("supplierid")]
    public int Supplierid { get; set; }

    [Required(ErrorMessage = "The Supdesc field is required.")]
    [Column("supdesc")]
    public string Supdesc { get; set; } = null!;

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();
}

