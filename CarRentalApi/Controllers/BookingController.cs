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
        public async Task<IActionResult> CreateBooking([FromForm] Booking booking)
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


        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(Guid id)
        {
            var res = await ibookingRepository.GetByIdAsync(id);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var res = await ibookingRepository.DeleteByIdAsyncAsync(id);
            return Ok(res);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking( [FromBody] Booking booking)
        {
            if (booking.BookingId == null)
            {
                return BadRequest("Booking ID mismatch");
            }
            var updatedBooking = await ibookingRepository.UpdateBookingAsync(booking);
            return Ok(updatedBooking);
        }
    }
}

