using AutoMapper;
using FluentAssertions;
using Moq;
using RedArbor.Application.Commands;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Tests.Helpers;

namespace RedArbor.Tests.CreateEmployeCommandTests
{
    /// <summary>
    /// Tests para CreateEmployeeCommand
    /// Verifica que el Metodo de creación funcione correctamente
    /// </summary>
    public class CreateEmployeCommandTests
    {
        private readonly Mock<IEmployesRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateEmployeeCommand _command;

        public CreateEmployeCommandTests()
        {
            // Arrange - Configuración común para todos los tests
            _repositoryMock = new Mock<IEmployesRepository>();
            _mapper = TestHelper.CreateMapper();
            _command = new CreateEmployeeCommand(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateEmploye()
        {
            // Arrange
            var createDto = TestHelper.CreateTestEmployeeDto();
            var expectedEmployee = TestHelper.CreateTestEmployee();

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Employe>()))
                .ReturnsAsync(expectedEmployee);

            // Act
            var result = await _command.CreateAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(expectedEmployee.Id);
            result.Email.Should().Be(expectedEmployee.Email);
            result.Username.Should().Be(expectedEmployee.Username);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Employe>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldMapDtoToEntity()
        {
            // Arrange
            var createDto = TestHelper.CreateTestEmployeeDto();
            Employe capturedEmployee = null!;

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Employe>()))
                .Callback<Employe>(emp => capturedEmployee = emp)
                .ReturnsAsync((Employe emp) => emp);

            // Act
            await _command.CreateAsync(createDto);

            // Assert
            capturedEmployee.Should().NotBeNull();
            capturedEmployee.CompanyId.Should().Be(createDto.CompanyId);
            capturedEmployee.Email.Should().Be(createDto.Email);
            capturedEmployee.Password.Should().Be(createDto.Password);
            capturedEmployee.Username.Should().Be(createDto.Username);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnDtoWithGeneratedId()
        {
            // Arrange
            var createDto = TestHelper.CreateTestEmployeeDto();
            var employeeWithId = TestHelper.CreateTestEmployee(999);

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Employe>()))
                .ReturnsAsync(employeeWithId);

            // Act
            var result = await _command.CreateAsync(createDto);

            // Assert
            result.Id.Should().Be(999);
        }

        [Fact]
        public async Task Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            // Act & Assert
            Action act = () => new CreateEmployeeCommand(null!, _mapper);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("repository");
        }

        [Fact]
        public async Task Constructor_ShouldThrow_WhenMapperIsNull()
        {
            // Act & Assert
            Action act = () => new CreateEmployeeCommand(_repositoryMock.Object, null!);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("mapper");
        }
    }
}