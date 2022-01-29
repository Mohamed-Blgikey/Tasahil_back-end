using System.ComponentModel.DataAnnotations;

namespace Tasahil_.net_core_6_.Models
{
    public class SavedPostsVM
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
    }
}
