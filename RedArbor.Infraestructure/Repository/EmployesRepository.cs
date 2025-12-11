using Dapper;
using Microsoft.EntityFrameworkCore;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Infraestructure.Context;

namespace RedArbor.Infraestructure.Repository
{
    
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
        /// Agrega un nuevo employe a la base de datos
        /// </summary>
        public async Task<Employe> AddAsync(Employe employee)
        {
            await _context.Employes.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        /// <summary>
        /// Actualiza un employe existente
        /// </summary>
        public async Task<bool> UpdateAsync(Employe employee)
        {
            var existingEmployee = await _context.Employes
                .FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (existingEmployee == null)
                return false;

            
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
            existingEmployee.DeleteOn = employee.DeleteOn;
            existingEmployee.Lastlogin = employee.Lastlogin;

            _context.Employes.Update(existingEmployee);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Elimina un employe de la base de datos
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
        /// Obtiene todos los employee usando Dapper
        /// </summary>
        public async Task<IEnumerable<Employe>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT 
                    Id, CompanyId, CreatedOn, DeleteOn, Email, 
                    Fax, Name, Lastlogin, Password, PortalId, 
                    RoleId, StatusId, Telephone, UpdatedOn, Username
                FROM Employes
                ORDER BY Id";

            return await connection.QueryAsync<Employe>(sql);
        }

        /// <summary>
        /// Obtiene un employe por ID usando Dapper
        /// </summary>
        public async Task<Employe?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT 
                    Id, CompanyId, CreatedOn, DeleteOn, Email, 
                    Fax, Name, Lastlogin, Password, PortalId, 
                    RoleId, StatusId, Telephone, UpdatedOn, Username
                FROM Employes
                WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<Employe>(sql, new { Id = id });
        }

        
        /// <summary>
        /// Verifica si existe un employe con el ID dado
        /// </summary>
        public async Task<bool> ExistsAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = "SELECT COUNT(1) FROM Employes WHERE Id = @Id";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            return count > 0;
        }

        #endregion
    } 
}