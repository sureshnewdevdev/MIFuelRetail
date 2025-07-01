using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RetailMasters.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<creditcard> creditcards { get; set; }

    public virtual DbSet<emailgroup> emailgroups { get; set; }

    public virtual DbSet<fueltype> fueltypes { get; set; }

    public virtual DbSet<message> messages { get; set; }

    public virtual DbSet<pricechange> pricechanges { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<site> sites { get; set; }

    public virtual DbSet<supplier> suppliers { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<valetprice> valetprices { get; set; }

    public virtual DbSet<valettype> valettypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<creditcard>(entity =>
        {
            entity.HasKey(e => e.cardid).HasName("creditcards_pkey");

            entity.Property(e => e.cardname).HasMaxLength(50);
            entity.Property(e => e.cardtype).HasMaxLength(30);
        });

        modelBuilder.Entity<emailgroup>(entity =>
        {
            entity.HasKey(e => e.groupid).HasName("emailgroups_pkey");

            entity.Property(e => e.groupname).HasMaxLength(50);
        });

        modelBuilder.Entity<fueltype>(entity =>
        {
            entity.HasKey(e => e.fueltypeid).HasName("fueltypes_pkey");

            entity.Property(e => e.fueltypename).HasMaxLength(50);
        });

        modelBuilder.Entity<message>(entity =>
        {
            entity.HasKey(e => e.messageid).HasName("messages_pkey");

            entity.Property(e => e.sentat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.subject).HasMaxLength(100);

            entity.HasOne(d => d.receiveruser).WithMany(p => p.messagereceiverusers)
                .HasForeignKey(d => d.receiveruserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_receiver");

            entity.HasOne(d => d.senderuser).WithMany(p => p.messagesenderusers)
                .HasForeignKey(d => d.senderuserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_sender");
        });

        modelBuilder.Entity<pricechange>(entity =>
        {
            entity.HasKey(e => e.changeid).HasName("pricechanges_pkey");

            entity.Property(e => e.changedate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.newprice).HasPrecision(10, 2);
            entity.Property(e => e.oldprice).HasPrecision(10, 2);

            entity.HasOne(d => d.fueltype).WithMany(p => p.pricechanges)
                .HasForeignKey(d => d.fueltypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pricechanges_fueltype");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.roleid).HasName("roles_pkey");

            entity.HasIndex(e => e.rolename, "roles_rolename_key").IsUnique();

            entity.Property(e => e.rolename).HasMaxLength(50);
        });

        modelBuilder.Entity<site>(entity =>
        {
            entity.HasKey(e => e.siteid).HasName("sites_pkey");

            entity.HasIndex(e => e.sitecode, "sites_sitecode_key").IsUnique();

            entity.Property(e => e.description).HasMaxLength(100);
            entity.Property(e => e.isactive).HasDefaultValue(true);
            entity.Property(e => e.shiftstatus).HasMaxLength(50);
            entity.Property(e => e.sitecode).HasMaxLength(20);

            entity.HasOne(d => d.supplier).WithMany(p => p.sites)
                .HasForeignKey(d => d.supplierid)
                .HasConstraintName("fk_sites_supplier");
        });

        modelBuilder.Entity<supplier>(entity =>
        {
            entity.HasKey(e => e.supplierid).HasName("suppliers_pkey");

            entity.Property(e => e.supdesc).HasMaxLength(100);
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.userid).HasName("users_pkey");

            entity.HasIndex(e => e.username, "users_username_key").IsUnique();

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.isactive).HasDefaultValue(true);
            entity.Property(e => e.username).HasMaxLength(50);

            entity.HasOne(d => d.role).WithMany(p => p.users)
                .HasForeignKey(d => d.roleid)
                .HasConstraintName("fk_users_role");
        });

        modelBuilder.Entity<valetprice>(entity =>
        {
            entity.HasKey(e => e.valetpriceid).HasName("valetprices_pkey");

            entity.Property(e => e.price).HasPrecision(10, 2);

            entity.HasOne(d => d.site).WithMany(p => p.valetprices)
                .HasForeignKey(d => d.siteid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_valetprices_site");

            entity.HasOne(d => d.valettype).WithMany(p => p.valetprices)
                .HasForeignKey(d => d.valettypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_valetprices_valettype");
        });

        modelBuilder.Entity<valettype>(entity =>
        {
            entity.HasKey(e => e.valettypeid).HasName("valettypes_pkey");

            entity.Property(e => e.valettypename).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
