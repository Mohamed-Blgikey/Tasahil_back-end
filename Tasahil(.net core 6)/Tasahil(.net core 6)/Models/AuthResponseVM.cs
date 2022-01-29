namespace Tasahil_.net_core_6_.Models
{
    public class AuthResponseVM
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
