using FinalProject_Service.Exceptions;
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
            if (file == null || file.Length == 0)
            {
                throw new CustomException(400, "Name", "Actor with this name already exists");
            }
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder, fileName);
            using FileStream fileStream = new(fullPath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
        public static bool CheckSize(this IFormFile file, int maxSize)
        {
            return file.Length >= maxSize;
        }
        public static bool CheckType(this IFormFile file, string[] types)
        {
            return types.Contains(file.ContentType);
        }
        public static bool DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
