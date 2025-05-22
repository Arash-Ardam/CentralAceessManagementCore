using ApplicationDbContext.EntityConfigs;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //  this for design-time
        public ApplicationDbContext() : base() { }


        public DbSet<DataCenter> DataCenters { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Database>()
                .HasKey(e => e.Name);

            modelBuilder.Entity<Database>()
                .Property<string>("DataBaseEngineName")
                .IsRequired();

            modelBuilder.Entity<Access>()
                .HasKey(ac => ac.Id);

            modelBuilder.Entity<Access>()
                .Property<string>("DataCenterName")
                .IsRequired();

            modelBuilder.ApplyConfiguration(new DataCenterConfigs());
            modelBuilder.ApplyConfiguration(new DatabaseEngineConfigs());
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=CentralAccessManagementCore;Integrated Security=SSPI;TrustServerCertificate=True");
        //}

    }
}
