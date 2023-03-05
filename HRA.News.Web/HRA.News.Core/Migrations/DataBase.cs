using Dapper;
using HRA.News.Core.ApplicationDbContext;
using HRA.News.Infrastructure.Interfaces;
using System.Linq;

namespace HRA.News.Core.Migrations
{
    public class Database : IDataBase
    {
        private readonly IDapperContext _context;
        public Database(IDapperContext context)
        {
            _context = context;
        }
        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            using (var connection = _context.CreateConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}

