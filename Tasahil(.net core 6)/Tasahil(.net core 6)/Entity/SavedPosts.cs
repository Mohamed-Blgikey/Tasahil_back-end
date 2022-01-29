using Tasahil_.net_core_6_.Extend;

namespace Tasahil_.net_core_6_.Entity
{
    public class SavedPosts
    {
        public SavedPosts()
        {
            SevedDate = DateTime.Now;
        }
        public int PostId { get; set; }
        public virtual Post Post { set; get; }
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { set; get; }
        public DateTime SevedDate { get; set; }
    }
}
