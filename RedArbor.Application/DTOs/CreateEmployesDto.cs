using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Application.DTOs
{
    /// <summary>
    /// DTO para crear un nuevo Employes
    /// Contiene todos los campos necesarios según el JSON de ejemplo del documento
    /// </summary>
    public class CreateEmployeeDto
    {
        // Campos obligatorios (NOT NULL según documento)
        public int CompanyId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string Username { get; set; } = string.Empty;

        // Campos opcionales (pueden ser NULL)
        public string? Name { get; set; }
        public string? Fax { get; set; }
        public string? Telephone { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? Lastlogin { get; set; }
    }
}
