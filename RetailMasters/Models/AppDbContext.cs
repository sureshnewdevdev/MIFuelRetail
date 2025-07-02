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

    public virtual DbSet<Creditcard> creditcards { get; set; }

    public virtual DbSet<Emailgroup> emailgroups { get; set; }

    public virtual DbSet<Fueltype> fueltypes { get; set; }

    public virtual DbSet<Message> messages { get; set; }

    public virtual DbSet<Pricechange> pricechanges { get; set; }

    public virtual DbSet<Role> roles { get; set; }

    public virtual DbSet<Site> sites { get; set; }

    public virtual DbSet<Supplier> suppliers { get; set; }

    public virtual DbSet<User> users { get; set; }

    public virtual DbSet<Valetprice> valetprices { get; set; }

    public virtual DbSet<Valettype> valettypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Creditcard>(entity =>
        {
            entity.HasKey(e => e.Cardid).HasName("creditcards_pkey");

            entity.Property(e => e.Cardname).HasMaxLength(50);
            entity.Property(e => e.Cardtype).HasMaxLength(30);
        });

        modelBuilder.Entity<Emailgroup>(entity =>
        {
            entity.HasKey(e => e.Groupid).HasName("emailgroups_pkey");

            entity.Property(e => e.Groupname).HasMaxLength(50);
        });

        modelBuilder.Entity<Fueltype>(entity =>
        {
            entity.HasKey(e => e.Fueltypeid).HasName("fueltypes_pkey");

            entity.Property(e => e.Fueltypename).HasMaxLength(50);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("messages_pkey");

            entity.Property(e => e.Sentat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Subject).HasMaxLength(100);

            entity.HasOne(d => d.Receiveruser).WithMany(p => p.MessageReceiverusers)
                .HasForeignKey(d => d.Receiveruserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_receiver");

            entity.HasOne(d => d.Senderuser).WithMany(p => p.MessageSenderusers)
                .HasForeignKey(d => d.Senderuserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_sender");
        });

        modelBuilder.Entity<Pricechange>(entity =>
        {
            entity.HasKey(e => e.Changeid).HasName("pricechanges_pkey");

            entity.Property(e => e.Changedate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Newprice).HasPrecision(10, 2);
            entity.Property(e => e.Oldprice).HasPrecision(10, 2);

            entity.HasOne(d => d.Fueltype).WithMany(p => p.Pricechanges)
                .HasForeignKey(d => d.Fueltypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pricechanges_fueltype");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roles_pkey");

            entity.HasIndex(e => e.Rolename, "roles_rolename_key").IsUnique();

            entity.Property(e => e.Rolename).HasMaxLength(50);
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Siteid).HasName("sites_pkey");

            entity.HasIndex(e => e.Sitecode, "sites_sitecode_key").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Shiftstatus).HasMaxLength(50);
            entity.Property(e => e.Sitecode).HasMaxLength(20);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Sites)
                .HasForeignKey(d => d.Supplierid)
                .HasConstraintName("fk_sites_supplier");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Supplierid).HasName("suppliers_pkey");

            entity.Property(e => e.Supdesc).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("fk_users_role");
        });

        modelBuilder.Entity<Valetprice>(entity =>
        {
            entity.HasKey(e => e.Valetpriceid).HasName("valetprices_pkey");

            entity.Property(e => e.Price).HasPrecision(10, 2);

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

            entity.Property(e => e.Valettypename).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
