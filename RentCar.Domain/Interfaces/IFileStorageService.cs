using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Infrastructure
{
    public interface IFileStorageService
    {
        Task<string> SaveImageAsync(IFormFile file, string folder);
        Task DeleteImageAsync(string filePath);
    }

}
