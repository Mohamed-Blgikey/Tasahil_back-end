using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class ForgetPassVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
