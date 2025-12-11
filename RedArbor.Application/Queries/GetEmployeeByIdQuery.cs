using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Interface;
using RedArbor.Domain.Interfaces;

namespace RedArbor.Application.Queries.GetEmployeeById
{
    /// <summary>
    /// Query para obtener un employee por su ID
    /// Implementa el patrón CQRS separando las lecturas
    /// </summary>
    public class GetEmployeeByIdQuery
    {
        private readonly IEmployesRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQuery(IEmployesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Ejecuta la query para obtener un employee específico
        /// </summary>
        /// <param name="id">ID del employee a buscar</param>
        /// <returns>EmployeeDto o null si no existe</returns>
        public async Task<EmployesDto?> ExecuteAsync(int id)
        {
            // 1. Obtener la entidad desde el repositorio (usa Dapper)
            var employee = await _repository.GetByIdAsync(id);

            // 2. Si no existe, retornar null
            if (employee == null)
                return null;

            // 3. Mapear la entidad de dominio a DTO usando AutoMapper
            return _mapper.Map<EmployesDto>(employee);
        }
    }
}