using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RedArbor.Infraestructure.Services
{
    /// <summary>
    /// Servicio para la generación de tokens JWT
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Genera un token JWT para un employe
        /// </summary>
        
        public string GenerateToken(Employe employee)
        {
            // Leer configuración JWT
            var secretKey = _configuration["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JWT SecretKey not configured");
            var issuer = _configuration["JwtSettings:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer not configured");
            var audience = _configuration["JwtSettings:Audience"]
                ?? throw new InvalidOperationException("JWT Audience not configured");
            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");

            // Crear los claims 
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, employee.Username),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim("CompanyId", employee.CompanyId.ToString()),
                new Claim("PortalId", employee.PortalId.ToString()),
                new Claim("RoleId", employee.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Crear la clave de seguridad
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            // Convertir el token a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}