using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repository
{
    public class BookingRepository: IBookingRepository
    {
        private readonly AppDbContext dbContext;

        public BookingRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            booking.BookingId = Guid.NewGuid();
            dbContext.Bookings.Add(booking);
            await dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingAsync()
        {
            return await dbContext.Bookings.ToListAsync();
        }
    }
}
