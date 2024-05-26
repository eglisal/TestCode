using MedicareApi.Models;

namespace MedicareApi.Services
{
    public interface IDbService
    {
        Task ExecuteAsync(string query, object value);
        Task<List<T>> GetAllAsync<T>(string command);
        Task<T> GetAsync<T>(string command, object parameters);
    }
}
