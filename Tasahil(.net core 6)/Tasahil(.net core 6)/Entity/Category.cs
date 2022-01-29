using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Entity
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { set; get; }
    }
}
