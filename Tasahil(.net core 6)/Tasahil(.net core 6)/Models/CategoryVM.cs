using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class CategoryVM
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
