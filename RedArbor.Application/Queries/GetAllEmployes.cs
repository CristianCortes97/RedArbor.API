using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Interface;

namespace RedArbor.Application.Queries
{

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
        /// Metodo para obtener todos los employees
        /// </summary>
        /// <returns>Lista de EmployeDto</returns>
        public async Task<IEnumerable<EmployesDto>> ExecuteAsync()
        {
            var employees = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<EmployesDto>>(employees);
        }
    }
}