using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RedArbor.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Infraestructure.Factories
{
   
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {          
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
        }

        /// <summary>
        /// Crea una nueva conexión SQL Server
        /// </summary>
        /// <returns>SqlConnection lista para usar con Dapper</returns>
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
