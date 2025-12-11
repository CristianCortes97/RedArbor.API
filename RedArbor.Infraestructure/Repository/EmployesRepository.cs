using Dapper;
using Microsoft.EntityFrameworkCore;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Domain.Interfaces;
using RedArbor.Infraestructure.Context;

namespace RedArbor.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para la entidad Employee
    /// Implementa escrituras con EF Core y lecturas con Dapper (patrón CQRS)
    /// </summary>
    public class EmployeeRepository : IEmployesRepository
    {
        private readonly DbredArborContext _context;
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public EmployeeRepository(
            DbredArborContext context,
            IDatabaseConnectionFactory connectionFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        #region Comandos - Escrituras con EF Core

        /// <summary>
        /// Agrega un nuevo employee a la base de datos
        /// </summary>
        public async Task<Employe> AddAsync(Employe employee)
        {
            await _context.Employes.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        /// <summary>
        /// Actualiza un employee existente
        /// </summary>
        public async Task<bool> UpdateAsync(Employe employee)
        {
            var existingEmployee = await _context.Employes
                .FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (existingEmployee == null)
                return false;

            // Actualizar todas las propiedades
            existingEmployee.CompanyId = employee.CompanyId;
            existingEmployee.Email = employee.Email;
            existingEmployee.Password = employee.Password;
            existingEmployee.PortalId = employee.PortalId;
            existingEmployee.RoleId = employee.RoleId;
            existingEmployee.StatusId = employee.StatusId;
            existingEmployee.Username = employee.Username;
            existingEmployee.Name = employee.Name;
            existingEmployee.Fax = employee.Fax;
            existingEmployee.Telephone = employee.Telephone;
            existingEmployee.CreatedOn = employee.CreatedOn;
            existingEmployee.UpdatedOn = employee.UpdatedOn;
            existingEmployee.DeletedOn = employee.DeletedOn;
            existingEmployee.Lastlogin = employee.Lastlogin;

            _context.Employes.Update(existingEmployee);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Elimina un employee de la base de datos
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employes.FindAsync(id);

            if (employee == null)
                return false;

            _context.Employes.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion

        #region Queries - Lecturas con Dapper

        /// <summary>
        /// Obtiene todos los employees usando Dapper
        /// </summary>
        public async Task<IEnumerable<Employe>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT 
                    Id, CompanyId, CreatedOn, DeletedOn, Email, 
                    Fax, Name, Lastlogin, Password, PortalId, 
                    RoleId, StatusId, Telephone, UpdatedOn, Username
                FROM Employees
                ORDER BY Id";

            return await connection.QueryAsync<Employe>(sql);
        }

        /// <summary>
        /// Obtiene un employee por ID usando Dapper
        /// </summary>
        public async Task<Employe?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT 
                    Id, CompanyId, CreatedOn, DeletedOn, Email, 
                    Fax, Name, Lastlogin, Password, PortalId, 
                    RoleId, StatusId, Telephone, UpdatedOn, Username
                FROM Employees
                WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<Employe>(sql, new { Id = id });
        }

        
        /// <summary>
        /// Verifica si existe un employee con el ID dado
        /// </summary>
        public async Task<bool> ExistsAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = "SELECT COUNT(1) FROM Employees WHERE Id = @Id";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            return count > 0;
        }

        #endregion
    } 
}