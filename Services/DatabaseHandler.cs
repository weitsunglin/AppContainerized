using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MySocketHttpApp.Services
{
    public class DatabaseHandler
    {
        private readonly string _connectionString;

        public DatabaseHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddRecordAsync(string tableName, string columnNames, string values)
        {
            var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({values})";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateRecordAsync(string tableName, string setClause, string condition)
        {
            var query = $"UPDATE {tableName} SET {setClause} WHERE {condition}";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteRecordAsync(string tableName, string condition)
        {
            var query = $"DELETE FROM {tableName} WHERE {condition}";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task ExecuteQueryAsync(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}
