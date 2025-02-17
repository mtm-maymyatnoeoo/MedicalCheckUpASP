using MedicalCheckUpASP.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalCheckUpASP.DbContexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) // Pass the options for SalesCategoryContext to the base constructor
        {
        }
        public DbSet<User> User { get; set; }

        // Override OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // If SalesCategory is meant to be keyless
            modelBuilder.Entity<User>().HasNoKey(); // Mark as keyless entity

            // Or if SalesCategory has a key, define it explicitly:
            // modelBuilder.Entity<SalesCategory>().HasKey(sc => sc.Id);
        }

        public void OpenConnection()
        {
            var connection = this.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        // Method to close the connection manually
        public void CloseConnection()
        {
            var connection = this.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        // Specific method for getting users using a query string
        public async Task<List<User>> GetSalesCategoryByQueryAsync(string query)
        {
            // Call the common SelectQueryAsync method from CommonContext
            //return await SelectQueryAsync<SalesCategory>(query);

            OpenConnection();  // Ensure the connection is open
            var dbSet = this.Set<User>();  // Access the DbSet for the entity T
            var result = await dbSet.FromSqlRaw(query).ToListAsync();

            CloseConnection(); // Close the connection after the query
            return result;
        }
       
    }

}
