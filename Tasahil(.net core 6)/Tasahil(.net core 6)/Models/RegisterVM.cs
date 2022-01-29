using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAgree { get; set; }
        [Required]
        public string PhotoName { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        [RegularExpression(@"^01[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$", ErrorMessage = "Digit Only and pattern 01(nine number)")]
        public string Phone { get; set; }
    }
}
