using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BlockLuster.Common;
using BlockLuster.EntityFramework;

namespace BlockLuster.Accessors.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>options)
            : base(options)
        { }

        public DatabaseContext()        
            :base()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = Config.SqlServerConnectionString;

            optionsBuilder.UseSqlServer(new SqlConnection
            {
                ConnectionString = connectionString
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRental>(ur => 
            {
                ur.Property(x => x.Id).HasColumnName("Id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            });
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<UserRental> UserRentals { get; set; }
    }
}
