using Microsoft.AspNetCore.Mvc;
using SampleProjectAPI.Contracts;
using SampleProjectAPI.Model.case_models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {

        private readonly ICaseRepository _caseRepo;
        public CaseController(ICaseRepository caseRepo)
        {
            _caseRepo = caseRepo;
        }

        // GET api/<CaseController>/5
        [HttpGet("activeEmployee")]
        public async Task<IActionResult> GetActiveEmployee([FromQuery(Name = "OldCaseCode")] string oldCaseCode)
        {
            try
            {
                var employees = await _caseRepo.GetActiveEmployee(oldCaseCode);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<CaseController>
        [HttpPost("/caseHistory")]
        public async Task<IActionResult> GetCaseHistory([FromBody] CaseHistoryRequest caseHistoryRequest)
        {
            try
            {
                var histories = await _caseRepo.GetCaseHistory(caseHistoryRequest);
                return Ok(histories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/updateCase")]
        public async Task<String> UpdateHistory([FromBody] UpdateCaseHistoryRequest updateCaseHistoryRequest)
        {
            try
            {
                await _caseRepo.UpdateEmployeeCaseHistory(updateCaseHistoryRequest);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed to Update the data";
            }
        }

        [HttpPost("/employeeUtilization")]
        public async Task<IActionResult> GetEmployeeUtilizationStatus([FromBody] CaseHistoryRequest caseHistoryRequest)
        {
            try
            {
                var status = await _caseRepo.GetEmployeeUtilizationStatus(caseHistoryRequest);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
