using System.ComponentModel.DataAnnotations;

namespace AdminFinal.ViewModel
{
    
        public class LogInVM
        {
            [Required]
            public string Email { get; set; } = null!;
            [Required]
            public string password { get; set; } = null!;
        }
    

}
