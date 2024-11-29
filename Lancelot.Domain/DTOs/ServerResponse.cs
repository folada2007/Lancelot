namespace Lancelot.Shared.DTOs
{
    public class ServerResponse
    {
        public Dictionary<string, string[]> ModelErrors { get; set; }
        public IEnumerable<string> IdentityErrors { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
