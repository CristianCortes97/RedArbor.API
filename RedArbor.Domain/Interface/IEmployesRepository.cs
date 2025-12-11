using RedArbor.Domain.Entities;

namespace RedArbor.Domain.Interface
{
    /// <summary>
    /// Interface del repositorio de Employe
    /// </summary>
    public interface IEmployesRepository
    {
        Task<Employe> AddAsync(Employe employee);
        Task<bool> UpdateAsync(Employe employee);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Employe>> GetAllAsync();
        Task<Employe?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}