using AutoMapper;
using FluentAssertions;
using Moq;
using RedArbor.Application.Queries;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Tests.Helpers;

namespace RedArbor.Tests.GetEmployeByQueryTests
{
    /// <summary>
    /// Tests para GetEmployeeByIdQuery
    /// Verifica que la query de obtener por ID funcione correctamente
    /// </summary>
    public class GetEmployeByIdQueryTests
    {
        private readonly Mock<IEmployesRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly GetEmployeeByIdQuery _query;

        public GetEmployeByIdQueryTests()
        {
            _repositoryMock = new Mock<IEmployesRepository>();
            _mapper = TestHelper.CreateMapper();
            _query = new GetEmployeeByIdQuery(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnEmployee_WhenIdExists()
        {
            // Arrange
            var employeeId = 1;
            var employee = TestHelper.CreateTestEmployee(employeeId);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(employee);

            // Act
            var result = await _query.ExecuteAsync(employeeId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(employeeId);
            result.Email.Should().Be(employee.Email);
            result.Username.Should().Be(employee.Username);
            _repositoryMock.Verify(r => r.GetByIdAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            var employeeId = 999;

            _repositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync((Employe?)null);

            // Act
            var result = await _query.ExecuteAsync(employeeId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldMapEntityToDto()
        {
            // Arrange
            var employeeId = 42;
            var employee = TestHelper.CreateTestEmployee(employeeId);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(employee);

            // Act
            var result = await _query.ExecuteAsync(employeeId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(employee.Id);
            result.CompanyId.Should().Be(employee.CompanyId);
            result.Email.Should().Be(employee.Email);
            result.Username.Should().Be(employee.Username);
            result.Name.Should().Be(employee.Name);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            // Act & Assert
            Action act = () => new GetEmployeeByIdQuery(null!, _mapper);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("repository");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenMapperIsNull()
        {
            // Act & Assert
            Action act = () => new GetEmployeeByIdQuery(_repositoryMock.Object, null!);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("mapper");
        }
    }
}