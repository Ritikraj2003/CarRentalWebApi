using Azure;
using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repository
{
    public class ConfirmBookingRepository:IConfirmBookingRepository
    {
        private readonly AppDbContext appContext;

        public ConfirmBookingRepository(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<ServiceResponse<ConfirmBooking>> CreateAsync(ConfirmBooking confirmBooking)
        {
            var response = new ServiceResponse<ConfirmBooking>();
            try
            {
                confirmBooking.ConfirmedAt = DateTime.UtcNow;
                appContext.ConfirmBooking.Add(confirmBooking);
                await appContext.SaveChangesAsync();

                response.Data = confirmBooking;
                response.Success = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                    response.Success= false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var res = await appContext.ConfirmBooking.FindAsync(id);
                if (res == null)
                {
                    response.Success = true;
                    response.Message = "Not Found";
                }

                response.Success= true;
                response.Message = "Deleted data";

            }
            catch (Exception ex)
            {
                response.Success= false;
                response.Message = ex.Message;  
            }
            return response;
        }

        public async Task<ServiceResponse<List<ConfirmBooking>>> GetAllAsync()
        {
            var response = new ServiceResponse<List<ConfirmBooking>>();
            try
            {
             var res =  await appContext.ConfirmBooking
            .Include(c => c.Booking)
            .Include(c => c.Driver)
            .ToListAsync();

                if (res == null)
                {
                    response.Success = true;
                    response.Message= "NOT found";
                }

                response.Data = res;
                response.Success= true;
                response.Message = "Sucess";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response ;
        }


        public async Task<ServiceResponse<ConfirmBooking>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<ConfirmBooking>();
            try
            {
                var confirm = await appContext.ConfirmBooking
               .Include(c => c.Booking)
               .Include(c => c.Driver)
               .FirstOrDefaultAsync(c => c.Id == id);
                if (confirm == null)
                    response.Message = "Not Found Data!";

                response.Data = confirm;
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;   
            }
            return response;
           
                
        }

        

        public async Task<ServiceResponse<ConfirmBooking>> UpdateAsync(int id, ConfirmBooking updated)
        {
           var response = new ServiceResponse<ConfirmBooking>();
            try
            {

                var existing = await appContext.ConfirmBooking.FindAsync(id);
                if (existing == null)
                {

                        response.Success = false;
                       response.Message = "Confirm booking not found.";

                }

                existing.BookingId = updated.BookingId;
                existing.DriverId = updated.DriverId;
                existing.Status = updated.Status;

                await appContext.SaveChangesAsync();
                response.Data =existing;
                response.Success = true;
                response.Message = "Confirm booking updated successfully.";

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
