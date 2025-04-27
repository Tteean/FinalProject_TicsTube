using FinalProject_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_ViewModel.ViewModels
{
    public class ProfileVm
    {
        public ProfileUpdateVm ProfileUpdateVm { get; set; }
        public List<Order> Orders { get; set; }
    }
}
