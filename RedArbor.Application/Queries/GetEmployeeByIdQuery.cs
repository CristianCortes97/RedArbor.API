using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Interface;

namespace RedArbor.Application.Queries
{

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
        ///  Metodo para obtener un employe específico
        /// </summary>
        /// <param name="id">ID del employe a buscar</param>
        /// <returns>EmployeDto o null si no existe</returns>
        public async Task<EmployesDto?> ExecuteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                return null;

            return _mapper.Map<EmployesDto>(employee);
        }
    }
}