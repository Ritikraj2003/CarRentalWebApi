using CarRentalApi.DbContext;
using CarRentalApi.GmailService;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmBookingController : ControllerBase
    {
        private readonly IConfirmBookingRepository confirmBookingRepository;
        private readonly AppDbContext appDbContext;
        private readonly EmailService emailService;

        public ConfirmBookingController(IConfirmBookingRepository confirmBookingRepository,AppDbContext appDbContext, EmailService emailService)
        {
            this.confirmBookingRepository = confirmBookingRepository;
            this.appDbContext = appDbContext;
            this.emailService = emailService;
        }
    

    [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await confirmBookingRepository.GetByIdAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConfirmBooking model)
        {
            try
            {
                var booking = await appDbContext.Bookings.FindAsync(model.BookingId);
                var driver = await appDbContext.Drivers.FindAsync(model.DriverId);

                if (booking == null || driver == null)
                    return BadRequest(new { Success = false, Message = "Invalid Booking or Driver" });

                var result = await confirmBookingRepository.CreateAsync(model);

                string subject = "Your Booking is Confirmed!";
                string body = $@"
            <h2>Hello {booking.Name},</h2>  
            <p>Your booking has been confirmed:</p>
            <ul>
                <li><strong>Pickup Location:</strong> {booking.PickupLocation}</li>
                <li><strong>Drop Location:</strong> {booking.DropLocation}</li>
                <li><strong>Driver Name:</strong> {driver.Name}</li>
                <li><strong>Driver Phone:</strong> {driver.Phone}</li>
                <li><strong>Driver Email:</strong> {driver.gmail}</li>
            </ul>
            <p>Thank you for booking with us!</p>";

                 await emailService.SendEmailAsync(booking.Email, subject, body, true);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConfirmBooking model)
        {
            try
            { 
                var response = await confirmBookingRepository.UpdateAsync(id, model);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await confirmBookingRepository.DeleteAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

    }
}
