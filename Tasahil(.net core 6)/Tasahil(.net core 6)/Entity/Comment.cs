using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasahil_.net_core_6_.Extend;

namespace Tasahil_.net_core_6_.Entity
{
    public class Comment
    {
        public Comment()
        {
            CommentDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { set; get; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { set; get; }
        [Required]
        [MaxLength(1000)]
        public string CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
