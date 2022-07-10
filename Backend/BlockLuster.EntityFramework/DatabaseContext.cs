using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BlockLuster.Common;

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
    }
}
