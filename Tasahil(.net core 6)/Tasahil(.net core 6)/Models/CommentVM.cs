using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class CommentVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }


        [Required]
        public int PostId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string CommentContent { get; set; }
    }
}
