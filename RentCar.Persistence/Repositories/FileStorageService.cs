using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RentCar.Infrastructure
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IHostingEnvironment _env;

        public FileStorageService(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string folder)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, folder);
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine(folder, fileName).Replace("\\", "/"); // relative path
        }

        public Task DeleteImageAsync(string filePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath, filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }
    }

}
