namespace Tasahil_.net_core_6_.Helper
{
    public class ApiResponse<Type>
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public Type Data { get; set; }
        public Type Error { get; set; }
    }
}
