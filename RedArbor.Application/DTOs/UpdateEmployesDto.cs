using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Application.DTOs
{
    /// <summary>
    /// DTO para actualizar un Employee existente
    /// Similar a CreateEmployeeDto pero sin el Id (viene en la URL)
    /// </summary>
    public class UpdateEmployeeDto
    {
        // Campos obligatorios
        public int CompanyId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string Username { get; set; } = string.Empty;

        // Campos opcionales
        public string? Name { get; set; }
        public string? Fax { get; set; }
        public string? Telephone { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? Lastlogin { get; set; }
    }
}
