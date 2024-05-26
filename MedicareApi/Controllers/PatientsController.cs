using MedicareApi.Models;
using MedicareApi.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace MedicareApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors("SpecificPolicy")]

    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;
        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            try
            {
                var result = await _patientService.GetPatientsAsync();
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when querying patients: {ex}");

                return StatusCode(500, "An error occurred while processing the request. Please try again later.");

            }
        }
    }

}
