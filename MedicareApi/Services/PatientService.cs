using Dapper;
using MedicareApi.Controllers;
using MedicareApi.Models;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using System.Data;

namespace MedicareApi.Services
{
    public class PatientService:IPatientService
    {
        private readonly IDbService _dbService;
        private readonly ILogger<PatientsController> _logger;
        public PatientService(IDbService dbService, ILogger<PatientsController> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            try
            {
                var query = @"WITH PatientEncounters AS (
            SELECT 
                    p.id,
                    p.first_name,
                    p.last_name,
                    p.age,
                    COUNT(e.id) AS EncounterCount,
                    string_agg(DISTINCT f.city, ', ') AS VisitedCities,
                    COUNT(DISTINCT payers.city) AS DifferentInsuranceCities
                FROM patients p
                INNER JOIN encounters e ON p.id = e.patient_id
                INNER JOIN facilities f ON e.facility_id = f.id
                INNER JOIN payers payers ON e.payer_id = payers.id
                GROUP BY p.id, p.first_name, p.last_name, p.age
            )
            SELECT 
                pe.last_name || ', ' || pe.first_name AS FullName,
                pe.VisitedCities,
                CASE WHEN pe.age < 16 THEN 'A' ELSE 'B' END AS Category
            FROM PatientEncounters pe
            WHERE pe.EncounterCount >= 2 AND pe.DifferentInsuranceCities >= 2
            ORDER BY pe.EncounterCount ASC";


                return await _dbService.GetAllAsync<Patient>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when querying patients. GetPatientsAsync: {ex}");
                throw ex;
            }

        }
    }
}
