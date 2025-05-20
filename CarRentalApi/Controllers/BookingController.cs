using CarRentalApi.Interface;
using CarRentalApi.Models;
using CarRentalApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingRepository ibookingRepository;
        private readonly EmailService emailService;

        public BookingController(IBookingRepository IbookingRepository,EmailService emailService)
        {

            ibookingRepository = IbookingRepository;
            this.emailService = emailService;
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] Booking booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }
            var res = await ibookingRepository.AddBookingAsync(booking);

            string subject = "New Booking Created";
            //string body = $"A new booking has been made by {booking.Name}.\n\n" +
            //              $"Details:\nBookingId: {booking.BookingId}\nName: {booking.Name}\nEmail: {booking.Email}\nPhone: {booking.Phone_no}\n" +
            //              $"CarType: {booking.cartype}\nBookingType: {booking.BookingType}\n" +
            //              $"PickupLocation: {booking.PickupLocation}\nPickupDate: {booking.PickupDate}\nPickupTime: {booking.PickupTime}\n"+
            //              $"DropLocation: {booking.DropLocation} \nDropDate: {booking.Dropdate} \n DropTime: {booking.Droptime}";
            string body = $@"
     <html>
    <body>
        <p>A new booking has been made by <strong>{booking.Name}</strong>.</p>
        <p><strong>Details:</strong><br/>
        BookingId: {booking.BookingId}<br/>
        Name: {booking.Name}<br/>
        Email: {booking.Email}<br/>
        Phone: {booking.Phone_no}<br/>
        CarType: {booking.cartype}<br/>
        BookingType: {booking.BookingType}<br/>
        PickupLocation: {booking.PickupLocation}<br/>
        PickupDate: {booking.PickupDate}<br/>
        PickupTime: {booking.PickupTime}<br/>
        DropLocation: {booking.DropLocation}<br/>
        DropDate: {booking.Dropdate}<br/>
        DropTime: {booking.Droptime}</p>

        <p style='color: yellow; background-color: black; font-weight: bold; padding: 10px;'>
            Auto generated email
        </p>
    </body>
    </html>";

            await emailService.SendEmailAsync("ritikraj1092002@gmail.com", subject, body, isHtml: true);
            return Ok(res);
        }
         
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await ibookingRepository.GetAllBookingAsync();
            return Ok(cars);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(Guid id)
        {
            var res = await ibookingRepository.GetByIdAsync(id);
            return Ok(res);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var res = await ibookingRepository.DeleteByIdAsyncAsync(id);
            return Ok(res);
        }

        [Authorize]
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

