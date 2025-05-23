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

        [HttpGet]
        public async Task<IActionResult> GetAllDriver()
        {
            try
            {
                var res = await driverRepository.GetAllDriversAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByDriverID(int id)
        {
            try
            {
                var res = await driverRepository.GetDriverByIdAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, Driver driver)
        {
            try
            {
                var res = await driverRepository.UpdateDriverAsync(id, driver);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var res = await driverRepository.DeleteDriverAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
