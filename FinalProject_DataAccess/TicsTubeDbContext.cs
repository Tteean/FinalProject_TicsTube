using FinalProject_Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_DataAccess
{
    public class TicsTubeDbContext : DbContext
    {
        DbSet<Setting> Settings { get; set; }
        public TicsTubeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
