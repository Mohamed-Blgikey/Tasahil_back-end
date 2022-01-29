using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class LoginVM
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
