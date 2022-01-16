using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace royal_car_rentals_web_api.Models
{
    public partial class RoyalCarRentalsDBContext : DbContext
    {
        public RoyalCarRentalsDBContext()
        {
        }

        public RoyalCarRentalsDBContext(DbContextOptions<RoyalCarRentalsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingLog> BookingLogs { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Inquiry> Inquiries { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehicleMaker> VehicleMakers { get; set; } = null!;
        public virtual DbSet<VehicleModel> VehicleModels { get; set; } = null!;
        public virtual DbSet<VerificationCode> VerificationCodes { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-IK08IC5\\SQLEXPRESS;Database=RoyalCarRentalsDB;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Role).HasMaxLength(100);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Bookings_Cities");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Bookings_Customers");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Bookings_Drivers");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Bookings_Vehicles");
            });

            modelBuilder.Entity<BookingLog>(entity =>
            {
                entity.Property(e => e.Action)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingLogs)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingLogs_Bookings");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityName).HasMaxLength(100);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LicenceNo).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.ProfilePicture).HasMaxLength(100);

                entity.Property(e => e.VerificationStatus).HasMaxLength(100);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LicenceNo).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.ProfilePicture).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drivers_Cities");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_Feedbacks_Bookings");
            });

            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Message).HasMaxLength(500);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_Payments_Bookings");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(100);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.ImagesPath).HasMaxLength(100);

                entity.Property(e => e.RegistrationNumber).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_Cities");

                entity.HasOne(d => d.Maker)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.MakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_VehicleMakers");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_VehicleModels");
            });

            modelBuilder.Entity<VehicleMaker>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(100);
            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.HasOne(d => d.Maker)
                    .WithMany(p => p.VehicleModels)
                    .HasForeignKey(d => d.MakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleModels_VehicleMakers");
            });

            modelBuilder.Entity<VerificationCode>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
