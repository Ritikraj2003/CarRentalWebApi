using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTypeController : ControllerBase
    {
        private readonly IbookingTypeRepository ibookingTypeRepository;

        public BookingTypeController(IbookingTypeRepository ibookingTypeRepository)
        {
            this.ibookingTypeRepository = ibookingTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookingType()
        {
            var res = await ibookingTypeRepository.GetAllBookingTypeAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookingType([FromForm] BookingType bookingType)
        {
             var res = await ibookingTypeRepository.AddBookingTypeAsync(bookingType);
            return Ok(res);
        }
    }
}
