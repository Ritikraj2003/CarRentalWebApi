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

        public CarRepository(AppDbContext context, IWebHostEnvironment environment, IHttpContextAccessor  httpContextAccessor)
        {
            _context = context;
            _environment = environment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> AddCarAsync(Car car, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                car.ImagePath = await SaveImageAsync(imageFile);
            }

            car.CarId = Guid.NewGuid();
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

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var folderPath = Path.Combine(_environment.WebRootPath ?? "wwwroot", "Uploads");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var fullPath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Build full URL
            var request = httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";

            return $"{baseUrl}/Uploads/{uniqueFileName}";
        }

        public  async Task<Car> GetByCarId(Guid id)
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
    }

}
