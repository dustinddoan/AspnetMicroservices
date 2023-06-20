using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Discount.API.Extensions
{
    public class PostgresMigration
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public PostgresMigration(IConfiguration configuration, ILogger<PostgresMigration> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public void MigrateDatabase()
        {
            _logger.LogInformation("Start Migrating Postgres Database");
            using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            // using var command = new NpgsqlCommand{ Connection = connection};
            connection.Execute("DROP TABLE IF EXISTS MyTable");

            connection.Execute(@"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                    ProductName VARCHAR(24),
                                                    Description TEXT,
                                                    Amount INT)"
                                                    );

            connection.Execute(@"INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('IPhone X', 'IPhone Discount', 150)");
            connection.Execute(@"INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Samsung 10', 'Samsung Discount', 100)");


            _logger.LogInformation("Complete Migrating Postgres Database");

        }

        public bool TableExists(string tableName, NpgsqlConnection connection)
        {
            var sql = "SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = @TableName)";
            var tableExists = connection.ExecuteScalar<bool>(sql, new { TableName = tableName });
            return tableExists;
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

    }
}