using System.ComponentModel.DataAnnotations;

namespace AreaApi.Domain.Models.DTOs
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "User Name Is required")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; }
    }
}
