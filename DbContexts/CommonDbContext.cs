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

        //public CommonDbContext(DbContextOptions options) : base(options)
        //{
        //}

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

        // Method to open the connection manually
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

        // A common method for executing select queries (raw SQL)
        public async Task<List<T>> SelectQueryAsync<T>(string query, params object[] parameters) where T : class
        {
            OpenConnection();  // Ensure the connection is open
            var dbSet = this.Set<T>();  // Access the DbSet for the entity T
            var result = await dbSet.FromSqlRaw(query, parameters).ToListAsync();

            CloseConnection(); // Close the connection after the query
            return result;
        }

        // Define your tables (DbSets)
        //public DbSet<SalesCategory> SalesCategories { get; set; }
        ////private readonly string _connectionString;
        //private MySqlConnection _connection;
        //private readonly DbContext _context;

        //public CommonDbContext(DbContext context)
        //{
        //    _context = context;
        //    //_connectionString = connectionString;
        //    //_connection = new MySqlConnection(_connectionString);
        //    var _connection = _context.Database.GetDbConnection();

        //}

        //public void OpenConnection()
        //{
        //    if (_connection.State == System.Data.ConnectionState.Closed)
        //    {
        //        _connection.Open();
        //    }
        //}

        //public void CloseConnection()
        //{
        //    if (_connection.State == System.Data.ConnectionState.Open)
        //    {
        //        _connection.Close();
        //    }
        //}

        //public async Task<List<T>> SelectQueryAsync<T>(string query, params object[] parameters)
        //{
        //    //List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

        //    try
        //    {
        //        OpenConnection();
        //        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Dictionary<string, object> row = new Dictionary<string, object>();

        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    row[reader.GetName(i)] = reader.GetValue(i);
        //                }

        //                resultList.Add(row);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Database Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }

        //    return resultList;
        //}
    }
}
