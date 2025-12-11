using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Domain.Interface
{

    /// <summary>
    /// Interface para crear conexiones a la base de datos
    /// Permite abstraer la creación de conexiones para Dapper
    /// </summary>
    public interface IDatabaseConnectionFactory
    {
        /// <summary>
        /// Crea una nueva conexión a la base de datos
        /// </summary>
        /// <returns>Una conexión IDbConnection lista para usar</returns>
        IDbConnection CreateConnection();
    }
}
