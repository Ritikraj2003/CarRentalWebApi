using CarRentalApi.GmailService;
using CarRentalApi.Interface;
using CarRentalApi.Models;
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
        private readonly GmailBody gmailBody;

        public BookingController(IBookingRepository IbookingRepository,EmailService emailService,GmailBody gmailBody)
        {

            ibookingRepository = IbookingRepository;
            this.emailService = emailService;
            this.gmailBody = gmailBody;
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
           
            var body= await gmailBody.SendEmail(booking);
            await emailService.SendEmailAsync("ritikraj1092002@gmail.com", subject, body, isHtml: true);

            // Send email to the user
            var Body= await gmailBody.SendEmailToUser(booking);
            subject = "Booking Confirmation";
            await emailService.SendEmailAsync(booking.Email, subject, Body, isHtml: true);
            return Ok(res);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await ibookingRepository.GetAllBookingAsync();
            return Ok(cars);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var res = await ibookingRepository.GetByIdAsync(id);
            return Ok(res);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var res = await ibookingRepository.DeleteByIdAsyncAsync(id);
            return Ok(res);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking( int id,[FromBody] Booking booking)
        {
            if (booking.BookingId == null)
            {
                booking.BookingId = id;
            }
            var updatedBooking = await ibookingRepository.UpdateBookingAsync(booking);
            return Ok(updatedBooking);
        }
    }
}

