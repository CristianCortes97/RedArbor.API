using Microsoft.Extensions.Configuration;
using Dapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Domain.Interfaces;

namespace RedArbor.Application.Commands.Login
{
    /// <summary>
    /// Command para autenticar un usuario y generar un token JWT
    /// </summary>
    public class LoginCommand
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public LoginCommand(
            IDatabaseConnectionFactory connectionFactory,
            IJwtService jwtService,
            IConfiguration configuration)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Ejecuta el login: valida credenciales y genera token
        /// </summary>
        /// <param name="dto">Credenciales del usuario</param>
        /// <returns>LoginResponseDto con el token o null si las credenciales son inválidas</returns>
        public async Task<LoginResponseDto?> ExecuteAsync(LoginRequestDto dto)
        {
            // Buscar el employee por username y password usando Dapper
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT 
                    Id, CompanyId, CreatedOn, DeleteOn, Email, 
                    Fax, Name, Lastlogin, Password, PortalId, 
                    RoleId, StatusId, Telephone, UpdatedOn, Username
                FROM Employes
                WHERE Username = @Username AND Password = @Password";

            var employee = await connection.QueryFirstOrDefaultAsync<Employe>(
                sql,
                new { dto.Username, dto.Password }
            );

            // Si no se encuentra el employee, credenciales inválidas
            if (employee == null)
            {
                return null;
            }

            // Generar el token JWT
            var token = _jwtService.GenerateToken(employee);

            // Obtener tiempo de expiración desde configuración
            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");

            // Retornar la respuesta con el token
            return new LoginResponseDto
            {
                Token = token,
                Username = employee.Username,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes)
            };

        }
    }
}