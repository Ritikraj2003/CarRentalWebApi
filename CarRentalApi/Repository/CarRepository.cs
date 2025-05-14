namespace CarRentalApi.Repository
{
    using CarRentalApi.DbContext;
    using CarRentalApi.Interface;
    using CarRentalApi.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CarRepository(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var folderPath = Path.Combine(_environment.WebRootPath ?? "wwwroot", "Uploads");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var fullPath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return Path.Combine("Uploads", uniqueFileName).Replace("\\", "/");
        }
    }

}
