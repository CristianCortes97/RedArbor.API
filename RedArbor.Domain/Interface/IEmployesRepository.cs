using RedArbor.Domain.Entities;

namespace RedArbor.Domain.Interfaces
{
    /// <summary>
    /// Interface del repositorio de Employee
    /// Separa comandos (escrituras) de queries (lecturas) según CQRS
    /// </summary>
    public interface IEmployesRepository
    {
        // Comandos (Escrituras con EF Core)
        Task<Employe> AddAsync(Employe employee);
        Task<bool> UpdateAsync(Employe employee);
        Task<bool> DeleteAsync(int id);

        // Queries (Lecturas con Dapper)
        Task<IEnumerable<Employe>> GetAllAsync();
        Task<Employe?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}