using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class MangeRolesVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
