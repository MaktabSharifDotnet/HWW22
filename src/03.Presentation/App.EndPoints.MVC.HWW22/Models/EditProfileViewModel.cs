using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HWW22.Models
{
    public class EditProfileViewModel
    {
        [Display(Name = "نام کاربری")]
        public string Username { get; set; } 

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "شماره موبایل الزامی است")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string? Email { get; set; }
    }
}
