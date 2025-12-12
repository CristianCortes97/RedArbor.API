using Microsoft.AspNetCore.Mvc;
using RedArbor.Application.Commands.Login;
using RedArbor.Application.DTOs;

namespace RedArbor.API.Controllers
{
    /// <summary>
    /// Controller para autenticación
    /// Maneja el login y generación de tokens JWT
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginCommand _loginCommand;

        public AuthController(LoginCommand loginCommand)
        {
            _loginCommand = loginCommand ?? throw new ArgumentNullException(nameof(loginCommand));
        }

        /// <summary>
        /// POST /api/auth/login
        /// Autentica un usuario y devuelve un token JWT
        /// </summary>
        /// <param name="loginRequest">Credenciales del usuario</param>
        /// <returns>Token JWT si las credenciales son válidas</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                // Validar que el request no esté vacío
                if (string.IsNullOrWhiteSpace(loginRequest.Username) ||
                    string.IsNullOrWhiteSpace(loginRequest.Password))
                {
                    return BadRequest(new { message = "Username y Password son requeridos" });
                }

                // Ejecutar el comando de login
                var response = await _loginCommand.ExecuteAsync(loginRequest);

                // Si las credenciales son inválidas
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                // Retornar el token
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al procesar el login",
                    error = ex.Message
                });
            }
        }
    }
}