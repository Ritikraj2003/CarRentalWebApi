namespace CarRentalApi.Repository
{
    public class UploadImageRepository
    {

        public static async Task<string> UploadImageAsync(int baseFileName, IFormFile file, string subDirectory)
        {
            if (file == null || file.Length == 0)
                return null;

            string uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", subDirectory ?? string.Empty);

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            string fileExtension = Path.GetExtension(file.FileName);
            string uniqueFileName = $"{baseFileName}_{Guid.NewGuid()}{fileExtension}";
            string filePath = Path.Combine(uploadsDirectory, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // 👈 Async method
            }

            return filePath;
        }

    }
}
