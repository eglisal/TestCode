using Dapper;
using Npgsql;
using System.Data;

namespace MedicareApi.Services
{
    public class DbService:IDbService
    {

        private readonly IDbConnection _db;

        public DbService(IConfiguration configuration)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            _db = new NpgsqlConnection(connectionString);
        }

        public async Task ExecuteAsync(string query, object value)
        {
            await _db.ExecuteAsync(query, value);
        }
        
        public async Task<List<T>> GetAllAsync<T>(string command)
        {

            return (await _db.QueryAsync<T>(command).ConfigureAwait(false)).ToList();

        }

        public async Task<T> GetAsync<T>(string command, object parameters)
        {
            return await _db.QueryFirstOrDefaultAsync<T>(command, parameters);
        }


    }
}
