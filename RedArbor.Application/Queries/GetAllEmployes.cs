using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Interface;
using RedArbor.Domain.Interfaces;

namespace RedArbor.Application.Queries.GetAllEmployees
{
    /// <summary>
    /// Query para obtener todos los employees
    /// Implementa el patrón CQRS separando las lecturas
    /// </summary>
    public class GetAllEmployeesQuery
    {
        private readonly IEmployesRepository _repository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQuery(IEmployesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Ejecuta la query para obtener todos los employees
        /// </summary>
        /// <returns>Lista de EmployeeDto</returns>
        public async Task<IEnumerable<EmployesDto>> ExecuteAsync()
        {
            // 1. Obtener las entidades desde el repositorio (usa Dapper)
            var employees = await _repository.GetAllAsync();

            // 2. Mapear las entidades de dominio a DTOs usando AutoMapper
            return _mapper.Map<IEnumerable<EmployesDto>>(employees);
        }
    }
}