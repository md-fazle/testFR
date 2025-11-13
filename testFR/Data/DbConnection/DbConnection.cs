using Microsoft.AspNetCore.Mvc.Routing;
using System.Data;
using Microsoft.Data.SqlClient;

namespace testFR.Data.DbConnection
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
        }

        public SqlConnection CreateConnection() { 
        
                 return new SqlConnection(_connectionString);
        
        
        }


        public async Task<SqlConnection> CreateOpenConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = CreateConnection();
                await connection.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }


    }
}
