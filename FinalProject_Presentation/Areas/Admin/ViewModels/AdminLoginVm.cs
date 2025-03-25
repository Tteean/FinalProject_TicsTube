using System.ComponentModel.DataAnnotations;

namespace FinalProject_Presentation.Areas.Admin.ViewModels
{
    public class AdminLoginVm
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
