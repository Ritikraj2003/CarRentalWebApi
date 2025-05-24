namespace CarRentalApi.Repository
{
    using CarRentalApi.DbContext;
    using CarRentalApi.Interface;
    using CarRentalApi.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CarRepository(AppDbContext context, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _environment = environment;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task<ServiceResponse<Car>> AddCarAsync(Car car)
        {
            var response = new ServiceResponse<Car>();
            try
            {
                if (car.CarId == null)
                {
                    // car.CarId = Guid.NewGuid(); // ✅ Assign a new Guid if not set
                }
                if (car.ImageFile != null)
                {
                    // ✅ Safely use .Value because you've ensured it's not null
                    car.ImagePath = UploadImageRepository.UploadImageAsync(car.CarId, car.ImageFile, "car").Result;
                }
                _context.Cars.Add(car);
                _context.SaveChanges();
                response.Data = car;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteByCarId(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var car = _context.Cars.FirstOrDefault(c => c.CarId == id);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                    response.Data = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Car not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Car>>> GetAllCarsAsync()
        {
            var response = new ServiceResponse<List<Car>>();
            try
            {
                var cars = _context.Cars.ToList();
                response.Data = cars;
                response.Success = true;
                response.Message = "Cars retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Car>> GetByCarId(int id)
        {
            var response = new ServiceResponse<Car>();
            try
            {
                var car =   _context.Cars.FirstOrDefault(c => c.CarId == id);
                if (car != null)
                {
                    response.Data = car;
                    response.Success = true;
                    response.Message = "Car retrieved successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Car not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Car>> UpdateCarAsync(int id, Car car)
        {
            var response = new ServiceResponse<Car>();
            try
            {
                var existingCar = _context.Cars.FirstOrDefault(c => c.CarId == id);
                if (existingCar == null)
                {
                    response.Success = false;
                    response.Message = "Car not found";
                    return response;
                }
                if (car.ImageFile != null)
                {
                    car.ImagePath = UploadImageRepository.UploadImageAsync(existingCar.CarId, car.ImageFile, "car").Result;
                }
                // Update the properties
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                // Save the changes to the database
                _context.SaveChanges();
                response.Data = existingCar;
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
