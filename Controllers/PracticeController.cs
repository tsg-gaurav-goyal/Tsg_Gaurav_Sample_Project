using Microsoft.AspNetCore.Mvc;
using SampleProjectAPI.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {

        private readonly IPracticeRepository _practiceRepo;
        public PracticeController(IPracticeRepository practiceRepo)
        {
            _practiceRepo = practiceRepo;
        }

        // GET api/<PracticeController>/5
        [HttpGet("/getPracticeLeaders")]
        public async Task<IActionResult> GetCaseHistory([FromQuery(Name = "OfficeCode")] int officeCode)
        {
            try
            {
                var practiceLeaders = await _practiceRepo.GetPracticeLeaders(officeCode);
                return Ok(practiceLeaders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
