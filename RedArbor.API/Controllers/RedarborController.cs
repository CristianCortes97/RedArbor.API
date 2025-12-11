using Microsoft.AspNetCore.Mvc;
using RedArbor.Application.Commands;
using RedArbor.Application.DTOs;
using RedArbor.Application.Queries;
using RedArbor.Application.Validators;

namespace RedArbor.API.Controllers
{
    /// <summary>
    /// Controller para gestión de Employees
    /// </summary>
    [Route("api/redarbor")]
    [ApiController]
    public class RedarborController : ControllerBase
    {
        private readonly CreateEmployeeCommand _createCommand;
        private readonly UpdateEmployeeCommand _updateCommand;
        private readonly DeleteEmployeeCommand _deleteCommand;
        private readonly GetAllEmployeesQuery _getAllQuery;
        private readonly GetEmployeeByIdQuery _getByIdQuery;
        private readonly CreateEmployeeValidator _createValidator;
        private readonly UpdateEmployeeValidator _updateValidator;

        public RedarborController(
            CreateEmployeeCommand createCommand,
            UpdateEmployeeCommand updateCommand,
            DeleteEmployeeCommand deleteCommand,
            GetAllEmployeesQuery getAllQuery,
            GetEmployeeByIdQuery getByIdQuery,
            CreateEmployeeValidator createValidator,
            UpdateEmployeeValidator updateValidator)
        {
            _createCommand = createCommand ?? throw new ArgumentNullException(nameof(createCommand));
            _updateCommand = updateCommand ?? throw new ArgumentNullException(nameof(updateCommand));
            _deleteCommand = deleteCommand ?? throw new ArgumentNullException(nameof(deleteCommand));
            _getAllQuery = getAllQuery ?? throw new ArgumentNullException(nameof(getAllQuery));
            _getByIdQuery = getByIdQuery ?? throw new ArgumentNullException(nameof(getByIdQuery));
            _createValidator = createValidator ?? throw new ArgumentNullException(nameof(createValidator));
            _updateValidator = updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));
        }

        /// <summary>
        /// GET /api/redarbor
        /// Obtiene todos los employees
        /// </summary>
        /// <returns>Array of employe items</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployesDto>>> GetAll()
        {
            try
            {
                var employees = await _getAllQuery.ExecuteAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener los empleados",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// GET /api/redarbor/{id}
        /// Obtiene un employe por ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employe item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployesDto>> GetById(int id)
        {
            try
            {
                var employee = await _getByIdQuery.ExecuteAsync(id);

                if (employee == null)
                {
                    return NotFound(new { message = $"Employee con ID {id} no encontrado" });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener el empleado",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// POST /api/redarbor
        /// Crea un nuevo employe
        /// </summary>
        /// <param name="dto">Employe data</param>
        /// <returns>Created employe item</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployesDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployesDto>> Create([FromBody] CreateEmployeeDto dto)
        {
            try
            {
                // Realiza validaciones con fluentValidation
                var validationResult = await _createValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Errores de validación",
                        errors = validationResult.Errors.Select(e => new
                        {
                            field = e.PropertyName,
                            error = e.ErrorMessage
                        })
                    });
                }
             
                var createdEmployee = await _createCommand.CreateAsync(dto);
             
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdEmployee.Id },
                    createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al crear el empleado",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// PUT /api/redarbor/{id}
        /// Actualiza un employe existente
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="dto">Updated employe data</param>
        /// <returns>None (204 No Content)</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            try
            {
                // Realiza validaciones con FluentValidation
                var validationResult = await _updateValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Errores de validación",
                        errors = validationResult.Errors.Select(e => new
                        {
                            field = e.PropertyName,
                            error = e.ErrorMessage
                        })
                    });
                }

                var result = await _updateCommand.ExecuteAsync(id, dto);

                if (!result)
                {
                    return NotFound(new { message = $"Employee con ID {id} no encontrado" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar el empleado",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// DELETE /api/redarbor/{id}
        /// Elimina un employe
        /// </summary>
        /// <param name="id">Employe ID</param>
        /// <returns>None (204 No Content)</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _deleteCommand.ExecuteAsync(id);

                if (!result)
                {
                    return NotFound(new { message = $"Employee con ID {id} no encontrado" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al eliminar el empleado",
                    error = ex.Message
                });
            }
        }
    }
}