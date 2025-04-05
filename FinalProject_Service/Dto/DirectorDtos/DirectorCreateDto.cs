using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.DirectorDtos
{
    public class DirectorCreateDto
    {
        public string FullName { get; set; }
        public IFormFile File { get; set; }
    }
}
