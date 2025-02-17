using MedicalCheckUpASP.Models;
using Microsoft.EntityFrameworkCore;
//using MySql.Data.MySqlClient;

namespace MedicalCheckUpASP.DbContexts
{
    public class CommonDbContext: DbContext
    {
        public CommonDbContext(DbContextOptions<CommonDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // If SalesCategory is meant to be keyless
            //modelBuilder.Entity<User>().HasNoKey(); // Mark as keyless entity

            // Or if SalesCategory has a key, define it explicitly:
            // modelBuilder.Entity<SalesCategory>().HasKey(sc => sc.Id);
            modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<int>(); // Ensures enum is stored as INT
        }

     

        //// A common method for executing select queries (raw SQL)
        //public async Task<List<T>> SelectQueryAsync<T>(string query, params object[] parameters) where T : class
        //{
        //    OpenConnection();  // Ensure the connection is open
        //    var dbSet = this.Set<T>();  // Access the DbSet for the entity T
        //    var result = await dbSet.FromSqlRaw(query, parameters).ToListAsync();

        //    CloseConnection(); // Close the connection after the query
        //    return result;
        //}

    }
}
