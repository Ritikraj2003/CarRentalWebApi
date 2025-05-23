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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Car car)
        {
            if (car == null)
                return BadRequest();

            var result = await _carRepository.AddCarAsync(car);
            return CreatedAtAction(nameof(Get), new { id = result.CarId }, result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult>GetByCarId(Guid id)
        {
            var car = await _carRepository.GetByCarId(id); 
            return Ok(car); 
        }
        
        [HttpDelete ("{id}")]
        public async Task<IActionResult>DeleteById(Guid id)
        {
            var car= await _carRepository.DeleteByCarId(id);
            return Ok(car);
        }
       
        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateByCarId( Guid id , Car car)
        {
            var existingCar = await _carRepository.GetByCarId(id);

            if(existingCar!= null)
            {
                car.CarId = existingCar.CarId;
                var res = await _carRepository.UpdateCarAsync(car);
                return Ok(res);
            }
            return BadRequest("Not Found");
        }
    }

}
