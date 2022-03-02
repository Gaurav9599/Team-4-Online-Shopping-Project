using System.ComponentModel.DataAnnotations;

namespace ecommerse.ViewModel
{
    public class ForgotPwd
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }

}
