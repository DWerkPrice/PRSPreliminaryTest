using System;
using Microsoft.EntityFrameworkCore;
using PRSPreTestLibrary.Models;

namespace PRSPreTestLibrary
{
    public class AppDbContext : DbContext
    {
        //   private object value;
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Requestline> RequestLines { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
       
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//        public virtual DbSet<Vendor> Products { get; set; }// use plurals of class name, this has to be somewhere inside class this is the first of the lists of tables we can access
 //       public object Products { get; internal set; }
 //       public object Users { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if (!builder.IsConfigured) {
                builder.UseLazyLoadingProxies();
                var connStr = @"server=localhost\sqlexpress; database = PRSPreTest; trusted_connection = true;";
                builder.UseSqlServer(connStr);
            }
        }
        protected override void OnModelCreating(ModelBuilder model) {
            model.Entity<User>(e => {
                e.ToTable("Users");  //class name is singular and you should make table names plural
                e.HasKey(x => x.Id);
                e.Property(x => x.Username).HasMaxLength(30).IsRequired();
                e.Property(x => x.Password).HasMaxLength(30).IsRequired();
                e.Property(x => x.Firstname).HasMaxLength(30).IsRequired();
                e.Property(x => x.Lastname).HasMaxLength(30).IsRequired();
                e.Property(x => x.Phone).HasMaxLength(12);
                e.Property(x => x.Email).HasMaxLength(255);
                e.Property(x => x.IsReviewer).IsRequired().HasDefaultValue(false);
                e.Property(x => x.IsAdmin).IsRequired().HasDefaultValue(false);
                e.HasIndex(x => x.Username).IsUnique();
            });


            model.Entity<Vendor>(e => {
                e.ToTable("Vendors");  //class name is singular and you should make table names plural
                e.HasKey(x => x.Id);
                e.Property(x => x.Code).HasMaxLength(30).IsRequired();
                e.Property(x => x.Name).HasMaxLength(30).IsRequired();
                e.Property(x => x.Address).HasMaxLength(30).IsRequired();
                e.Property(x => x.City).HasMaxLength(30).IsRequired();
                e.Property(x => x.State).HasMaxLength(2).IsRequired();
                e.Property(x => x.Zip).HasMaxLength(5).IsRequired();
                e.Property(x => x.Phone).HasMaxLength(12);
                e.Property(x => x.Email).HasMaxLength(255);
                e.HasIndex(x => x.Code).IsUnique();
            });

            model.Entity<Product>(e => {
                e.ToTable("Products");
                e.HasKey(x => x.Id);
                e.Property(x => x.PartNbr).HasMaxLength(30).IsRequired();
                e.Property(x => x.Name).HasMaxLength(30).IsRequired();
                e.Property(x => x.Price).HasColumnType("decimal(5,2)").IsRequired();
                e.Property(x => x.Unit).HasMaxLength(30).IsRequired();
                e.Property(x => x.PhotoPath).HasMaxLength(255);
                e.HasOne(x => x.Vendor).WithMany(x => x.Products).HasForeignKey(x => x.VendorId).OnDelete(DeleteBehavior.Restrict);
                e.HasIndex(x => x.PartNbr).IsUnique();
            });

            //sets the individual properties in the SQL Database for Table Requests
            model.Entity<Request>(e => {
                e.ToTable("Requests");
                e.HasKey(x => x.Id);
                e.Property(x => x.Description).HasMaxLength(80).IsRequired();
                e.Property(x => x.Justification).HasMaxLength(80).IsRequired();
                e.Property(x => x.RejectionReason).HasMaxLength(80);
                e.Property(x => x.DeliveryMode).HasMaxLength(20).HasDefaultValue("Pickup");
                e.Property(x => x.Status).HasMaxLength(10).HasDefaultValue("New");
                e.Property(x => x.Total).HasColumnType("decimal(11,2)").HasDefaultValue("0");
                e.HasOne(x => x.User).WithMany(x => x.Requests).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

                //set the individual properties in the SQL Database for table RequestLine
                model.Entity<Requestline>(e => {
                    e.ToTable("RequestLines");
                    e.HasKey(x => x.Id);
                    e.Property(x => x.Quantity).HasDefaultValue("1");
                    e.HasOne(x => x.Request).WithMany(x => x.Requestlines).HasForeignKey(x => x.RequestId);
                    e.HasOne(x => x.Product).WithMany(x => x.Requestlines).HasForeignKey(x => x.ProductId);


                });
            });
        }
    }
}
