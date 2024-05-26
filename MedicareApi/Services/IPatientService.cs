using MedicareApi.Models;

namespace MedicareApi.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatientsAsync();
    }
}
