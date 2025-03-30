using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.ActorDtos
{
    public class ActorReturnDto
    {
        public string Fullname { get; set; }
        public IFormFile Photo { get; set; }
    }
}
