namespace RedArbor.Application.DTOs
{
    /// <summary>
    /// DTO para el request de login
    /// </summary>
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}