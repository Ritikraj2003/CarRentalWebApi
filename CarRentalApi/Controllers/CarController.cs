using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    using CarRentalApi.Interface;
    using CarRentalApi.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            return Ok(cars);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Car car)
        {
            try {
                if (car == null)
                    return BadRequest();

                var result = await _carRepository.AddCarAsync(car);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetByCarId(int id)
        {
            try { 
            var car = await _carRepository.GetByCarId(id); 
            return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete ("{id}")]
        public async Task<IActionResult>DeleteById(int id)
        {
            try { 
            var car= await _carRepository.DeleteByCarId(id);
            return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByCarId(int id, Car car)
        {
            try
            {
                var res = await _carRepository.UpdateCarAsync(id, car);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }

}
