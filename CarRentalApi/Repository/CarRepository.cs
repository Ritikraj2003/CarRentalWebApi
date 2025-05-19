namespace CarRentalApi.Repository
{
    using CarRentalApi.DbContext;
    using CarRentalApi.Interface;
    using CarRentalApi.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.EntityFrameworkCore;

    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor httpContextAccessor;
       
        public CarRepository(AppDbContext context, IWebHostEnvironment environment, IHttpContextAccessor  httpContextAccessor )
        {
            _context = context;
            _environment = environment;
            this.httpContextAccessor = httpContextAccessor;
           
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            if (car.CarId == null)
            {
                car.CarId = Guid.NewGuid(); // ✅ Assign a new Guid if not set
            }

            if (car.ImageFile != null)
            {
                // ✅ Safely use .Value because you've ensured it's not null
                car.ImagePath = await UploadImageRepository.UploadImageAsync(car.CarId.Value, car.ImageFile, "car");
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car;
        }



        //public async Task<string> SaveImageAsync(IFormFile imageFile)
        //{
        //    var folderPath = Path.Combine(_environment.WebRootPath ?? "wwwroot", "Uploads");
        //    if (!Directory.Exists(folderPath))
        //        Directory.CreateDirectory(folderPath);

        //    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        //    var fullPath = Path.Combine(folderPath, uniqueFileName);

        //    using (var stream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        await imageFile.CopyToAsync(stream);
        //    }

        //    return Path.Combine("Uploads", uniqueFileName).Replace("\\", "/");
        //}

        //public async Task<string> SaveImageAsync(IFormFile imageFile)
        //{
        //    var folderPath = Path.Combine(_environment.WebRootPath ?? "wwwroot", "Uploads");

        //    if (!Directory.Exists(folderPath))
        //        Directory.CreateDirectory(folderPath);

        //    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
        //    var fullPath = Path.Combine(folderPath, uniqueFileName);

        //    using (var stream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        await imageFile.CopyToAsync(stream);
        //    }

        //    // Build full URL
        //    var request = httpContextAccessor.HttpContext?.Request;
        //    var baseUrl = $"{request?.Scheme}://{request?.Host}";

        //    return $"{baseUrl}/Uploads/{uniqueFileName}";
        //}

        //public static string UploadImage(Guid newfileName, IFormFile file, string subDirectory)
        //{
        //    if (file == null || file.Length == 0)
        //        return null;
        //    string uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        //    if (!string.IsNullOrEmpty(subDirectory))
        //    {
        //        uploadsDirectory = Path.Combine(uploadsDirectory, subDirectory);
        //    }
        //    if (!Directory.Exists(uploadsDirectory))
        //    {
        //        Directory.CreateDirectory(uploadsDirectory);
        //    }
        //    newfileName = newfileName + "_" + Guid.NewGuid().ToString() + "." + file.FileName.Split('.').Last();
        //    string filePath = Path.Combine(uploadsDirectory, newfileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }
        //    return filePath;
        //}
       


        public async Task<Car> GetByCarId(Guid id)
        {
             var c = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
            return c;
        }

        public async Task<Car> DeleteByCarId(Guid id)
        {
             var c = await GetByCarId(id);
              _context.Cars.Remove(c);  
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            var existingCar = _context.Cars.FirstOrDefault(c => c.CarId == car.CarId);
            if (existingCar == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            if(car.ImageFile != null)
            {
                car.ImagePath = await UploadImageRepository.UploadImageAsync(existingCar.CarId.Value, car.ImageFile, "car");
            }


            // Update the properties
            _context.Entry(existingCar).CurrentValues.SetValues(car);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return existingCar;
        }
    }

}
