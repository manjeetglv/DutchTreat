using System.ComponentModel.DataAnnotations;

namespace DutchTreat.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}