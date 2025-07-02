using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;

namespace RetailMasters.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Areamanager> Areamanagers { get; set; }

    public virtual DbSet<Creditcard> Creditcards { get; set; }

    public virtual DbSet<Emailgroup> Emailgroups { get; set; }

    public virtual DbSet<Fuelprice> Fuelprices { get; set; }

    public virtual DbSet<Fueltype> Fueltypes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Nonmeteredsalestype> Nonmeteredsalestypes { get; set; }

    public virtual DbSet<Nozzle> Nozzles { get; set; }

    public virtual DbSet<Pricechange> Pricechanges { get; set; }

    public virtual DbSet<Pump> Pumps { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shopgroup> Shopgroups { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Tank> Tanks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Valetprice> Valetprices { get; set; }

    public virtual DbSet<Valettype> Valettypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ASoft;Username=admin;Password=admin123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Areamanager>(entity =>
        {
            entity.HasKey(e => e.Areamanagerid).HasName("areamanagers_pkey");

            entity.ToTable("areamanagers");

            entity.Property(e => e.Areamanagerid).HasColumnName("areamanagerid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Creditcard>(entity =>
        {
            entity.HasKey(e => e.Cardid).HasName("creditcards_pkey");

            entity.ToTable("creditcards");

            entity.Property(e => e.Cardid).HasColumnName("cardid");
            entity.Property(e => e.Cardname)
                .HasMaxLength(50)
                .HasColumnName("cardname");
            entity.Property(e => e.Cardtype)
                .HasMaxLength(30)
                .HasColumnName("cardtype");
        });

        modelBuilder.Entity<Emailgroup>(entity =>
        {
            entity.HasKey(e => e.Groupid).HasName("emailgroups_pkey");

            entity.ToTable("emailgroups");

            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Groupname)
                .HasMaxLength(50)
                .HasColumnName("groupname");
        });

        modelBuilder.Entity<Fuelprice>(entity =>
        {
            entity.HasKey(e => e.Fuelpriceid).HasName("fuelprices_pkey");

            entity.ToTable("fuelprices");

            entity.Property(e => e.Fuelpriceid).HasColumnName("fuelpriceid");
            entity.Property(e => e.Effectivedate).HasColumnName("effectivedate");
            entity.Property(e => e.Fueltypeid).HasColumnName("fueltypeid");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Siteid).HasColumnName("siteid");

            entity.HasOne(d => d.Fueltype).WithMany(p => p.Fuelprices)
                .HasForeignKey(d => d.Fueltypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fuelprices_fuel");

            entity.HasOne(d => d.Site).WithMany(p => p.Fuelprices)
                .HasForeignKey(d => d.Siteid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fuelprices_site");
        });

        modelBuilder.Entity<Fueltype>(entity =>
        {
            entity.HasKey(e => e.Fueltypeid).HasName("fueltypes_pkey");

            entity.ToTable("fueltypes");

            entity.Property(e => e.Fueltypeid).HasColumnName("fueltypeid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Fueltypename)
                .HasMaxLength(50)
                .HasColumnName("fueltypename");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.Messageid).HasColumnName("messageid");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Receiveruserid).HasColumnName("receiveruserid");
            entity.Property(e => e.Senderuserid).HasColumnName("senderuserid");
            entity.Property(e => e.Sentat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sentat");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .HasColumnName("subject");

            entity.HasOne(d => d.Receiveruser).WithMany(p => p.MessageReceiverusers)
                .HasForeignKey(d => d.Receiveruserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_receiver");

            entity.HasOne(d => d.Senderuser).WithMany(p => p.MessageSenderusers)
                .HasForeignKey(d => d.Senderuserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_sender");
        });

        modelBuilder.Entity<Nonmeteredsalestype>(entity =>
        {
            entity.HasKey(e => e.Salestypeid).HasName("nonmeteredsalestypes_pkey");

            entity.ToTable("nonmeteredsalestypes");

            entity.Property(e => e.Salestypeid).HasColumnName("salestypeid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Typename)
                .HasMaxLength(100)
                .HasColumnName("typename");
        });

        modelBuilder.Entity<Nozzle>(entity =>
        {
            entity.HasKey(e => e.Nozzleid).HasName("nozzles_pkey");

            entity.ToTable("nozzles");

            entity.Property(e => e.Nozzleid).HasColumnName("nozzleid");
            entity.Property(e => e.Nozzlenumber).HasColumnName("nozzlenumber");
            entity.Property(e => e.Pumpid).HasColumnName("pumpid");
            entity.Property(e => e.Tankid).HasColumnName("tankid");

            entity.HasOne(d => d.Pump).WithMany(p => p.Nozzles)
                .HasForeignKey(d => d.Pumpid)
                .HasConstraintName("fk_nozzles_pump");

            entity.HasOne(d => d.Tank).WithMany(p => p.Nozzles)
                .HasForeignKey(d => d.Tankid)
                .HasConstraintName("fk_nozzles_tank");
        });

        modelBuilder.Entity<Pricechange>(entity =>
        {
            entity.HasKey(e => e.Changeid).HasName("pricechanges_pkey");

            entity.ToTable("pricechanges");

            entity.Property(e => e.Changeid).HasColumnName("changeid");
            entity.Property(e => e.Changedate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("changedate");
            entity.Property(e => e.Fueltypeid).HasColumnName("fueltypeid");
            entity.Property(e => e.Newprice)
                .HasPrecision(10, 2)
                .HasColumnName("newprice");
            entity.Property(e => e.Oldprice)
                .HasPrecision(10, 2)
                .HasColumnName("oldprice");

            entity.HasOne(d => d.Fueltype).WithMany(p => p.Pricechanges)
                .HasForeignKey(d => d.Fueltypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pricechanges_fueltype");
        });

        modelBuilder.Entity<Pump>(entity =>
        {
            entity.HasKey(e => e.Pumpid).HasName("pumps_pkey");

            entity.ToTable("pumps");

            entity.Property(e => e.Pumpid).HasColumnName("pumpid");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Pumpcode)
                .HasMaxLength(50)
                .HasColumnName("pumpcode");
            entity.Property(e => e.Siteid).HasColumnName("siteid");

            entity.HasOne(d => d.Site).WithMany(p => p.Pumps)
                .HasForeignKey(d => d.Siteid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pumps_site");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Rolename, "roles_rolename_key").IsUnique();

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Shopgroup>(entity =>
        {
            entity.HasKey(e => e.Shopgroupid).HasName("shopgroups_pkey");

            entity.ToTable("shopgroups");

            entity.Property(e => e.Shopgroupid).HasColumnName("shopgroupid");
            entity.Property(e => e.Groupname)
                .HasMaxLength(100)
                .HasColumnName("groupname");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Siteid).HasName("sites_pkey");

            entity.ToTable("sites");

            entity.HasIndex(e => e.Sitecode, "sites_sitecode_key").IsUnique();

            entity.Property(e => e.Siteid).HasColumnName("siteid");
            entity.Property(e => e.Agreementno)
                .HasMaxLength(50)
                .HasColumnName("agreementno");
            entity.Property(e => e.Costafacility)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costafacility");
            entity.Property(e => e.Creditterms)
                .HasDefaultValue(0)
                .HasColumnName("creditterms");
            entity.Property(e => e.Customerref)
                .HasMaxLength(100)
                .HasColumnName("customerref");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Disableatm)
                .HasDefaultValue(false)
                .HasColumnName("disableatm");
            entity.Property(e => e.Disablecompanygoodspayout)
                .HasDefaultValue(false)
                .HasColumnName("disablecompanygoodspayout");
            entity.Property(e => e.Disablecompanynmp)
                .HasDefaultValue(false)
                .HasColumnName("disablecompanynmp");
            entity.Property(e => e.Disablecompanyonlinelottery)
                .HasDefaultValue(false)
                .HasColumnName("disablecompanyonlinelottery");
            entity.Property(e => e.Disableepay)
                .HasDefaultValue(false)
                .HasColumnName("disableepay");
            entity.Property(e => e.Disableinstantlottery)
                .HasDefaultValue(false)
                .HasColumnName("disableinstantlottery");
            entity.Property(e => e.Disablemotsales)
                .HasDefaultValue(false)
                .HasColumnName("disablemotsales");
            entity.Property(e => e.Disableonlinelottery)
                .HasDefaultValue(false)
                .HasColumnName("disableonlinelottery");
            entity.Property(e => e.Disableonlinelotterypaidout)
                .HasDefaultValue(false)
                .HasColumnName("disableonlinelotterypaidout");
            entity.Property(e => e.Disablepaypoint)
                .HasDefaultValue(false)
                .HasColumnName("disablepaypoint");
            entity.Property(e => e.Disablepaypointpayout)
                .HasDefaultValue(false)
                .HasColumnName("disablepaypointpayout");
            entity.Property(e => e.Disablepayzone)
                .HasDefaultValue(false)
                .HasColumnName("disablepayzone");
            entity.Property(e => e.Disableshopsales)
                .HasDefaultValue(false)
                .HasColumnName("disableshopsales");
            entity.Property(e => e.Hideinpricechange)
                .HasDefaultValue(false)
                .HasColumnName("hideinpricechange");
            entity.Property(e => e.Insurance)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("insurance");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Marketingservices)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("marketingservices");
            entity.Property(e => e.Penceperlitre)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("penceperlitre");
            entity.Property(e => e.Postcode)
                .HasMaxLength(20)
                .HasColumnName("postcode");
            entity.Property(e => e.Shiftstatus)
                .HasMaxLength(50)
                .HasColumnName("shiftstatus");
            entity.Property(e => e.Shopcharge)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("shopcharge");
            entity.Property(e => e.Sitecode)
                .HasMaxLength(20)
                .HasColumnName("sitecode");
            entity.Property(e => e.Substopfacility)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("substopfacility");
            entity.Property(e => e.Subwayfacility)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("subwayfacility");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Valetcharge)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("valetcharge");
            entity.Property(e => e.Vwcountryfacility)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("vwcountryfacility");
            entity.Property(e => e.Workcharge)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("workcharge");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Sites)
                .HasForeignKey(d => d.Supplierid)
                .HasConstraintName("fk_sites_supplier");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Supplierid).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Supdesc)
                .HasMaxLength(100)
                .HasColumnName("supdesc");
        });

        modelBuilder.Entity<Tank>(entity =>
        {
            entity.HasKey(e => e.Tankid).HasName("tanks_pkey");

            entity.ToTable("tanks");

            entity.Property(e => e.Tankid).HasColumnName("tankid");
            entity.Property(e => e.Capacity)
                .HasPrecision(10, 2)
                .HasColumnName("capacity");
            entity.Property(e => e.Fueltypeid).HasColumnName("fueltypeid");
            entity.Property(e => e.Siteid).HasColumnName("siteid");
            entity.Property(e => e.Tankcode)
                .HasMaxLength(50)
                .HasColumnName("tankcode");

            entity.HasOne(d => d.Fueltype).WithMany(p => p.Tanks)
                .HasForeignKey(d => d.Fueltypeid)
                .HasConstraintName("fk_tanks_fuel");

            entity.HasOne(d => d.Site).WithMany(p => p.Tanks)
                .HasForeignKey(d => d.Siteid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tanks_site");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("fk_users_role");
        });

        modelBuilder.Entity<Valetprice>(entity =>
        {
            entity.HasKey(e => e.Valetpriceid).HasName("valetprices_pkey");

            entity.ToTable("valetprices");

            entity.Property(e => e.Valetpriceid).HasColumnName("valetpriceid");
            entity.Property(e => e.Effectivedate).HasColumnName("effectivedate");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Siteid).HasColumnName("siteid");
            entity.Property(e => e.Valettypeid).HasColumnName("valettypeid");

            entity.HasOne(d => d.Site).WithMany(p => p.Valetprices)
                .HasForeignKey(d => d.Siteid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_valetprices_site");

            entity.HasOne(d => d.Valettype).WithMany(p => p.Valetprices)
                .HasForeignKey(d => d.Valettypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_valetprices_valettype");
        });

        modelBuilder.Entity<Valettype>(entity =>
        {
            entity.HasKey(e => e.Valettypeid).HasName("valettypes_pkey");

            entity.ToTable("valettypes");

            entity.Property(e => e.Valettypeid).HasColumnName("valettypeid");
            entity.Property(e => e.Valettypename)
                .HasMaxLength(50)
                .HasColumnName("valettypename");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
