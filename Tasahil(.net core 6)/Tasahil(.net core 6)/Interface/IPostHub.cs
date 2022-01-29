using Tasahil_.net_core_6_.Models;

namespace Tasahil_.net_core_6_.Interface
{
    public interface IPostHub
    {
        Task BrodcastPosts(PostVM postVM);
    }
}
