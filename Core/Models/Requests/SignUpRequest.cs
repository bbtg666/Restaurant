using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests
{
    public class SignUpRequest
    {
        [Required]
        public string UserName { get; set; }
        [Compare("ConfirmPassword", ErrorMessage ="Password not match!")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number.")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { set; get; }
    }
}
