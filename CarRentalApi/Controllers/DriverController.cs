using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository driverRepository;

        public DriverController(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(Driver driver)
        {
            try
            {
                var res = await driverRepository.AddDriverAsync(driver);
                return Ok(res);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex);
                }

            }
        }
    }
}
