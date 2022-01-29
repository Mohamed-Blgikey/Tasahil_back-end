using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Tasahil_.net_core_6_.Entity;

namespace Tasahil_.net_core_6_.Extend
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string PhotoName { get; set; }
        public bool IsAgree { get; set; }
        public virtual ICollection<Post> Posts { set; get; }
        public virtual ICollection<Comment> Comments { set; get; }
        public virtual ICollection<SavedPosts> SavedPosts { set; get; }
    }
}
