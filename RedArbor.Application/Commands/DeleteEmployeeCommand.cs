using RedArbor.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Application.Commands
{
    public class DeleteEmployeeCommand
    {
        private readonly IEmployesRepository _repository;

        public DeleteEmployeeCommand(IEmployesRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Metodo para Eliminar un empleado existente
        /// </summary>
        /// <param name="id">id a eliminar</param>
        /// <returns>204 no content</returns>
        public async Task<bool> ExecuteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
