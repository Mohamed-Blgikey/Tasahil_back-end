using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasahil_.net_core_6_.Extend;

namespace Tasahil_.net_core_6_.Entity
{
    public class Post
    {
        public Post()
        {
            PuplishDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime PuplishDate { get; set; }
        public int? CateId { get; set; }
        [ForeignKey("CateId")]
        public virtual Category? Category { set; get; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { set; get; }
        public string PhotoName { get; set; }
        public virtual ICollection<Comment> Comments { set; get; }
        public virtual ICollection<SavedPosts> SavedPosts { set; get; }
    }
}
