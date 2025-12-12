using RedArbor.Domain.Entities;

namespace RedArbor.Domain.Interface
{
    /// <summary>
    /// Interface para el servicio de generación de tokens JWT
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Genera un token JWT para un employe autenticado
        /// </summary>
        /// <param name="employee">name employe autenticado</param>
        /// <returns>Token JWT como string</returns>
        string GenerateToken(Employe employee);
    }
}