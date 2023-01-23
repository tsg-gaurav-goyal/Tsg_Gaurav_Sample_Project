using Microsoft.AspNetCore.Mvc;
using SampleProjectAPI.Contracts;
using SampleProjectAPI.Model.employee_model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }


        // POST api/<ValuesController1>
        [HttpPost]
        public async Task<IActionResult> GetCompaniesById([FromBody] MinimalModel ids)
        {
            try
            {
                var employees = await _employeeRepo.GetEmployeesById(ids);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/updateEmployeePosition")]
        public async Task<IActionResult> UpdateEmployeePositionHistory([FromBody] UpdateEmployeePositionRequest updateEmployeePositionRequest)
        {
            try
            {
                var updatedHistory = await _employeeRepo.UpdateEmployeePosition(updateEmployeePositionRequest);
                return Ok(updatedHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

