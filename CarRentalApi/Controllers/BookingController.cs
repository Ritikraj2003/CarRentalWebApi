using CarRentalApi.Interface;
using CarRentalApi.Models;
using CarRentalApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        
        private readonly IBookingRepository ibookingRepository;

        public BookingController(IBookingRepository IbookingRepository)
        {
           
            ibookingRepository = IbookingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking( [FromForm] Booking booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }
             var res = await ibookingRepository.AddBookingAsync(booking);
            return Ok(res);

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await ibookingRepository.GetAllBookingAsync();
            return Ok(cars);
        }
    }
}

