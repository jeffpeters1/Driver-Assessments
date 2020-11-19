using Driver.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driver.INFRASTRUCTURE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CORE.Entities.Driver> Drivers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Assessment> Assessments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CORE.Entities.Driver>(ConfigureDriver);
            builder.Entity<Company>(ConfigureCompany);
            builder.Entity<Assessment>(ConfigureAssessment);


            base.OnModelCreating(builder);
        }

        private static void ConfigureDriver(EntityTypeBuilder<CORE.Entities.Driver> builder)
        {
            builder.ToTable("Drivers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.HasUKLicence).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();

            // one-to-many relationship 
            builder.HasMany(x => x.Assessments).WithOne(e => e.Driver).HasForeignKey(e => e.DriverId).IsRequired();
        }

        private static void ConfigureCompany(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(x => x.Id);

            // one-to-many relationship 
            builder.HasMany(x => x.Drivers).WithOne(e => e.Company).HasForeignKey(e => e.CompanyId).IsRequired();
        }

        private static void ConfigureAssessment(EntityTypeBuilder<Assessment> builder)
        {
            builder.ToTable("Assessments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TestDate).IsRequired();
            builder.Property(x => x.Examiner).IsRequired();
            builder.Property(x => x.PassedDriving).IsRequired();
            builder.Property(x => x.PassedTheory).IsRequired();
            builder.Property(x => x.PassedEyeExam).IsRequired();
        }
    }
}
