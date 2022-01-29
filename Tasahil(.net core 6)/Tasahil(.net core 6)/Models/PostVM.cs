using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class PostVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [Range(100, 1000000)]
        public double Price { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public int CateId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string PhotoName { get; set; }
    }
}
