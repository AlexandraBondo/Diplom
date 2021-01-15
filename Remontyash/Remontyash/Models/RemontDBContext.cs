using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Remontyash.Models//Сгенерированная фигня от Entity Framework
{
    public partial class RemontDBContext : DbContext
    {
        public RemontDBContext()
        {
        }

        public RemontDBContext(DbContextOptions<RemontDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Emp> Emps { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TypeJob> TypeJobs { get; set; }
        public virtual DbSet<TypeTechnic> TypeTechnics { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-SFKRIAM\\MSSQLSERVER01; Database=RemontDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.Fio).IsRequired();
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.Property(e => e.Empid).ValueGeneratedNever();

                entity.Property(e => e.Fio).IsRequired();

                entity.Property(e => e.Position).IsRequired();
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.ClientId, "IX_ClientId");

                entity.HasIndex(e => e.Empid, "IX_Empid");

                entity.HasIndex(e => e.TypeJobId, "IX_TypeJobId");

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.Accepted).HasColumnType("datetime");

                entity.Property(e => e.Completed).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_dbo.Orders_dbo.Clients_ClientId");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Empid)
                    .HasConstraintName("FK_dbo.Orders_dbo.Emps_Empid");

                entity.HasOne(d => d.TypeJob)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TypeJobId)
                    .HasConstraintName("FK_dbo.Orders_dbo.TypeJobs_TypeJobId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TypeJob>(entity =>
            {
                entity.HasIndex(e => e.TypeTechnicId, "IX_TypeTechnicId");

                entity.Property(e => e.TypeJobId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.TypeTechnic)
                    .WithMany(p => p.TypeJobs)
                    .HasForeignKey(d => d.TypeTechnicId)
                    .HasConstraintName("FK_dbo.TypeJobs_dbo.TypeTechnics_TypeTechnicId");
            });

            modelBuilder.Entity<TypeTechnic>(entity =>
            {
                entity.Property(e => e.TypeTechnicId).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Userid)
                    .ValueGeneratedNever()
                    .HasColumnName("USERID");

                entity.Property(e => e.Empid).HasColumnName("EMPID");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Roleid).HasColumnName("ROLEID");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Empid)
                    .HasConstraintName("FK_Users_Emps");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
