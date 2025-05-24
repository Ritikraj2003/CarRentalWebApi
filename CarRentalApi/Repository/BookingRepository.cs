using Azure.Core;
using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext dbContext;
        public BookingRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ServiceResponse<Booking>> AddBookingAsync(Booking booking)
        {
            var response = new ServiceResponse<Booking>();
            try
            {
                dbContext.Bookings.Add(booking);
                await dbContext.SaveChangesAsync();
                response.Data = booking;
                response.Success = true;
                response.Message = "Booking added successfully.";
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteByIdAsyncAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var booking = await dbContext.Bookings.FindAsync(id);
                if (booking == null)
                {
                    response.Success = false;
                    response.Message = "Booking not found.";
                }
                else
                {
                    dbContext.Bookings.Remove(booking);
                    await dbContext.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Booking deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Booking>>> GetAllBookingAsync()
        {
            var response = new ServiceResponse<List<Booking>>();
            try
            {
                var bookings = dbContext.Bookings.ToList();
                response.Data = bookings;
                response.Success = true;
                response.Message = "Bookings retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Booking>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<Booking>();
            try
            {
                var booking = dbContext.Bookings.FirstOrDefault(c => c.BookingId == id);
                if (booking == null)
                {
                    response.Success = false;
                    response.Message = "Booking not found.";
                }
                else
                {
                    response.Data = booking;
                    response.Success = true;
                    response.Message = "Booking retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Booking>> UpdateBookingAsync(int id,Booking booking)
        {
            var response = new ServiceResponse<Booking>();
            try
            {
                var existingBooking = dbContext.Bookings.FirstOrDefault(b => b.BookingId == id);
                if (existingBooking == null)
                {
                    throw new InvalidOperationException("Booking Id Not Found");
                    response.Success = false;
                    response.Message = "Booking Not Found";
                    return response;
                }
                
                booking.BookingId = id; // add id
                // Update the properties
                dbContext.Entry(existingBooking).CurrentValues.SetValues(booking);
                // Save the changes to the database
                await dbContext.SaveChangesAsync();
                response.Data = existingBooking;
                response.Success = true;
                response.Message = "Booking updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

