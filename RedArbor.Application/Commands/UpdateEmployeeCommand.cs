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
    /// Command para actualizar un Employee existente
    /// Responsabilidad: Coordinar la actualización de un employee usando el repositorio
    /// </summary>
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
        /// Ejecuta el comando para actualizar un employee
        /// </summary>
        /// <param name="id">ID del employee a actualizar</param>
        /// <param name="dto">Nuevos datos del employee</param>
        /// <returns>True si se actualizó correctamente, False si no se encontró</returns>
        public async Task<bool> ExecuteAsync(int id, UpdateEmployeeDto dto)
        {
            // Mapear DTO a entidad de dominio usando AutoMapper
            var employee = _mapper.Map<Employe>(dto);

            // Asignar el ID que viene de la URL
            employee.Id = id;

            // Actualizar en la base de datos usando EF Core
            return await _repository.UpdateAsync(employee);
        }
    }
}
