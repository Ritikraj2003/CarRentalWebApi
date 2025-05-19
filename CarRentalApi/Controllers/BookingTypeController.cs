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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByBookingTypeId(Guid id)
        {
            var res = await ibookingTypeRepository.GetByBookingTypeId(id);
            return Ok(res);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult>DeleteBYBookingTypeId(Guid id)
        {
            var res = await ibookingTypeRepository.DeleteByBookingTypeId(id);
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingType( [FromBody] BookingType bookingType)
        {
            if ( bookingType.BookingTypeId == null)
            {
                return BadRequest("BookingType ID Not found");
            }
            var res = await ibookingTypeRepository.UpdateBookingTypeAsync(bookingType);
            return Ok(res);
        }
    }
}
