using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Load connection string and configure EF Core with SQL Server and retry logic
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// 2. Register your repositories/services
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddHttpContextAccessor();

// 3. Add controller services
builder.Services.AddControllers();

// 4. Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Enable Swagger only in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 6. Serve static files (for uploaded images like /Uploads/image.jpg)
app.UseStaticFiles();

// 7. Enable HTTPS redirection and authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// 8. Map controller endpoints
app.MapControllers();

// 9. Run the application
app.Run();
