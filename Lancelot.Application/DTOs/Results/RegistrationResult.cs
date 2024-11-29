namespace Lancelot.Application.DTOs
{
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public Dictionary<string,string> Errors { get; set; }
    }
}
