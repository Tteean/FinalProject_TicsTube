using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Extentions
{
    public static class FileManager
    {
        public static string SaveImage(this IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder, fileName);
            using FileStream fileStream = new(fullPath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
    }
}
