namespace RedArbor.Application.DTOs
{
    /// <summary>
    /// DTO para la respuesta de login con el token JWT
    /// </summary>
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}