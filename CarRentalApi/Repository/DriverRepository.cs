using Azure;
using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repository
{
    public class DriverRepository: IDriverRepository
    {
        private readonly AppDbContext appDbContext;

        public DriverRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<ServiceResponse<Driver>> AddDriverAsync(Driver driver)
        {
            ServiceResponse<Driver> DriverData = new ServiceResponse<Driver>();

            try
            {
                 await appDbContext.Drivers.AddAsync(driver);
                 await appDbContext.SaveChangesAsync();
               
                    DriverData.Data = driver;
                    DriverData.Success = true;
                    DriverData.Message = "Successfully added";
                
            }
            catch (Exception ex) 
                {
                   DriverData.Success = false;
                   DriverData.Message = ex.Message; 

                }
            return DriverData;

        }

        public async Task<ServiceResponse<bool>> DeleteDriverAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var driver = await appDbContext.Drivers.FindAsync(id);

                if (driver == null)
                {
                    response.Success = false;
                    response.Message = "Driver not found.";
                }
                else
                {
                    appDbContext.Drivers.Remove(driver); 
                    await appDbContext.SaveChangesAsync();

                    response.Success = true;
                    response.Data = true;
                    response.Message = "Driver deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<List<Driver>>> GetAllDriversAsync()
        {
            var response = new ServiceResponse<List<Driver>>();

            try
            {
                var drivers = await appDbContext.Drivers.ToListAsync();
                response.Data = drivers;
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<Driver>> GetDriverByIdAsync(Guid id)
        {
            var response = new ServiceResponse<Driver>();

            try
            {
                var driver = await appDbContext.Drivers.FindAsync(id);

                if (driver == null)
                {
                    response.Success = false;
                    response.Message = "Driver not found.";
                }
                else
                {
                    response.Data = driver;
                    response.Message = "Driver retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<Driver>> UpdateDriverAsync(Driver driver)
        {
            var response = new ServiceResponse<Driver>();

            try
            {
                var existingDriver = await appDbContext.Drivers.FindAsync(driver.DriverID);

                if (existingDriver == null)
                {
                    response.Success = false;
                    response.Message = "Driver not found.";
                    return response;
                }
                existingDriver.Name = driver.Name;
                existingDriver.Phone = driver.Phone;
                existingDriver.Address = driver.Address;

                await appDbContext.SaveChangesAsync();

                response.Data = existingDriver;
                response.Success = true;
                response.Message = "Driver updated successfully.";
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
