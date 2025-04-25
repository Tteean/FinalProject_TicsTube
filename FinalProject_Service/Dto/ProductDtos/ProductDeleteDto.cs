using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.ProductDtos
{
    public class ProductDeleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public bool InStock { get; set; }
        public IFormFile? File { get; set; }
        public List<int> MovieId { get; set; }
        public List<int> TVShowId { get; set; }
    }
}
