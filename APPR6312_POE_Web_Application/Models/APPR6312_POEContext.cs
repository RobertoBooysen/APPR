using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using APPR6312_POE_Web_Application.Models;

#nullable disable

namespace APPR6312_POE_Web_Application.Models
{
    public partial class APPR6312_POEContext : DbContext
    {
        public APPR6312_POEContext()
        {
        }

        public APPR6312_POEContext(DbContextOptions<APPR6312_POEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdmin> TblAdmins { get; set; }
        public virtual DbSet<TblCategory> TblCategories { get; set; }
        public virtual DbSet<TblDisaster> TblDisasters { get; set; }
        public virtual DbSet<TblGoodsDonation> TblGoodsDonations { get; set; }
        public virtual DbSet<TblMonetaryDonation> TblMonetaryDonations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:robertobooysenmain.database.windows.net,1433;Initial Catalog=APPR6312_POE;Persist Security Info=False;User ID=robertobooysenmain;Password=Roberto18;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__tblAdmin__F3DBC5739A6253A1");

                entity.ToTable("tblAdmin");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoriesId)
                    .HasName("PK__tblCateg__52A9711819F0C6A8");

                entity.ToTable("tblCategories");

                entity.Property(e => e.CategoriesId).HasColumnName("categoriesID");

                entity.Property(e => e.CategoryNames)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("categoryNames");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblDisaster>(entity =>
            {
                entity.HasKey(e => e.DisasterId)
                    .HasName("PK__tblDisas__8FE448946093FDBB");

                entity.ToTable("tblDisaster");

                entity.Property(e => e.DisasterId).HasColumnName("disasterID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblGoodsDonation>(entity =>
            {
                entity.HasKey(e => e.GoodsId)
                    .HasName("PK__tblGoods__110ED9D25661CF6A");

                entity.ToTable("tblGoodsDonations");

                entity.Property(e => e.GoodsId).HasColumnName("goodsID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("full_Name");

                entity.Property(e => e.NumberOfItems).HasColumnName("number_of_items");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblMonetaryDonation>(entity =>
            {
                entity.HasKey(e => e.DonationId)
                    .HasName("PK__tblMonet__F7F4F433E92FB062");

                entity.ToTable("tblMonetaryDonations");

                entity.Property(e => e.DonationId).HasColumnName("donationID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("full_Name");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<APPR6312_POE_Web_Application.Models.TblAllocateMoney> TblAllocateMoney { get; set; }

        public DbSet<APPR6312_POE_Web_Application.Models.TblAllocateGoods> TblAllocateGoods { get; set; }

        public DbSet<APPR6312_POE_Web_Application.Models.TblInventory> TblInventory { get; set; }

        public DbSet<APPR6312_POE_Web_Application.Models.TblAllocateInventory> TblAllocateInventory { get; set; }
    }
}
