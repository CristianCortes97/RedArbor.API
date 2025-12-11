using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Application.Commands
{

    /// <summary>
    /// Command para crear un nuevo Employee
    /// Responsabilidad: Coordinar la creación de un employee usando el repositorio
    /// </summary>
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
        /// Ejecuta el comando para crear un employee
        /// </summary>
        /// <param name="dto">Datos del employee a crear</param>
        /// <returns>Employee creado con su ID asignado</returns>
        public async Task<EmployesDto> CreateAsync(CreateEmployeeDto dto)
        {
            // Mapear DTO a entidad de dominio usando AutoMapper
            var employee = _mapper.Map<Employe>(dto);

            // Guardar en la base de datos usando EF Core
            var createdEmploye = await _repository.AddAsync(employee);

            // Mapear la entidad guardada a DTO de respuesta usando AutoMapper
            return _mapper.Map<EmployesDto>(createdEmploye);
        }
    }
}
