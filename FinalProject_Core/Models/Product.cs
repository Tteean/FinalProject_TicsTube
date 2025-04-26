using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Product:Audit
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public bool InStock { get; set; }
        public string Image { get; set; }
        public List<MovieProduct> MovieProducts { get; set; }
        public List<TvShowProduct> tvShowProducts { get; set; }
        public Product()
        {
            MovieProducts = new List<MovieProduct>();
            tvShowProducts = new List<TvShowProduct>();
        }


    }
}
