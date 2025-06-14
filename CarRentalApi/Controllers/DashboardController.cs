using CarRentalApi.Interface;
using CarRentalApi.Models;
using CarRentalApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IbookingTypeRepository ibookingTypeRepository;
        private readonly ICarRepository carRepository;
        private readonly IContactRepository contactRepository;
        private readonly IDriverRepository driverRepository;

        public DashboardController(IBookingRepository bookingRepository,
                                   IbookingTypeRepository ibookingTypeRepository,
                                   ICarRepository carRepository,
                                   IContactRepository contactRepository,
                                   IDriverRepository driverRepository)
        {
            this.bookingRepository = bookingRepository;
            this.ibookingTypeRepository = ibookingTypeRepository;
            this.carRepository = carRepository;
            this.contactRepository = contactRepository;
            this.driverRepository = driverRepository;
        }

        [HttpGet("total-count")]
        public async Task<IActionResult> GetTotalCount()
        {
            try
            {
                var cars = await carRepository.GetAllCarsAsync();
                var bookings = await bookingRepository.GetAllBookingAsync();
                var drivers = await driverRepository.GetAllDriversAsync();
                var bookingTypes = await ibookingTypeRepository.GetAllBookingTypeAsync();
                var contacts = await contactRepository.GetAllAsync();

                var totalCount = new
                {
                    Total_Car = cars?.Data?.Count ?? 0,
                    Total_Booking = bookings?.Data?.Count ?? 0,
                    Total_Driver = drivers?.Data?.Count ?? 0,
                    Total_bookingtype = bookingTypes?.Data?.Count ?? 0,
                    Total_contacts = contacts?.Data?.Count ?? 0,

                    Bookingfor = new Bookingfor
                    {
                        company = bookings?.Data?.Count(b => b.CompanyEnabled == true) ?? 0,
                        Indiviusal = bookings?.Data?.Count(b => b.CompanyEnabled == false) ?? 0
                    }
                };

                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while retrieving totals.",
                    Error = ex.Message
                });
            }
        }

    }
}
