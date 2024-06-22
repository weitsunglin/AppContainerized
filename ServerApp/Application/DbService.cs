using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace MyApp.Services
{
    public class DbService
    {
        private readonly string _connectionString;

        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsync(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            var result = new List<Dictionary<string, object>>();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                result.Add(row);
            }
            return result;
        }
        
        // Create Table
        public async Task CreateTableAsync(string tableName, Dictionary<string, string> columns)
        {
            var columnDefinitions = string.Join(", ", columns.Select(kvp => $"{kvp.Key} {kvp.Value}"));
            var query = $"CREATE TABLE {tableName} ({columnDefinitions})";

            await ExecuteNonQueryAsync(query);
        }

        // Insert Data
        public async Task InsertDataAsync(string tableName, Dictionary<string, object> data)
        {
            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Values.Select(value => $"'{value}'"));
            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

            await ExecuteNonQueryAsync(query);
        }

        // Update Data
        public async Task UpdateDataAsync(string tableName, Dictionary<string, object> data, string whereClause)
        {
            var setClauses = string.Join(", ", data.Select(kvp => $"{kvp.Key} = '{kvp.Value}'"));
            var query = $"UPDATE {tableName} SET {setClauses} WHERE {whereClause}";

            await ExecuteNonQueryAsync(query);
        }

        // Delete Data
        public async Task DeleteDataAsync(string tableName, string whereClause)
        {
            var query = $"DELETE FROM {tableName} WHERE {whereClause}";

            await ExecuteNonQueryAsync(query);
        }
    }
}