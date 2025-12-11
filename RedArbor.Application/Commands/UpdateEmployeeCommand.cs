using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Application.Commands
{

    public class UpdateEmployeeCommand
    {
        private readonly IEmployesRepository _repository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommand(IEmployesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Metodo para actualizar un employe
        /// </summary>
        /// <param name="id">ID del employe a actualizar</param>
        /// <param name="dto">Nuevos datos del employe</param>
        /// <returns>True si se actualizó correctamente, False si no se encontró</returns>
        public async Task<bool> ExecuteAsync(int id, UpdateEmployeeDto dto)
        {

            var employee = _mapper.Map<Employe>(dto);


            employee.Id = id;

            return await _repository.UpdateAsync(employee);
        }
    }
}
