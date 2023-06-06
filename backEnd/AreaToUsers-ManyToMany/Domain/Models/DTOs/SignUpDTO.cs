using System.ComponentModel.DataAnnotations;

namespace AreaApi.Domain.Models.DTOs
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "User Name Is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email Is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Password Is required")]
        public string PasswordConfirm { get; set; }
    }
}
