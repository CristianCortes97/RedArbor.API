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

    public class CreateEmployeeCommand
    {
        private readonly IEmployesRepository _repository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommand(IEmployesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Metodo para crear un nuevo empleado
        /// </summary>
        /// <param name="dto">Datos del employe a crear</param>
        /// <returns>Employe creado con su ID asignado</returns>
        public async Task<EmployesDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employe>(dto);

            var createdEmploye = await _repository.AddAsync(employee);

            return _mapper.Map<EmployesDto>(createdEmploye);
        }
    }
}
