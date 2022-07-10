using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BlockLuster.Common;
using BlockLuster.EntityFramework;

namespace BlockLuster.Accessors.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(new SqlConnection
            {
                ConnectionString = Config.SqlServerConnectionString
            });
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<UserRental> UserRentals { get; set; }
    }
}
