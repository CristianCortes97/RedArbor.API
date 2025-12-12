using AutoMapper;
using FluentAssertions;
using Moq;
using RedArbor.Application.Queries;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Tests.Helpers;

namespace RedArbor.Tests.GetAllEmployedQueryTests
{
    /// <summary>
    /// Tests para GetAllEmployeesQuery
    /// Verifica que la query de obtener todos funcione correctamente
    /// </summary>
    public class GetAllEmployesQueryTests
    {
        private readonly Mock<IEmployesRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly GetAllEmployeesQuery _query;

        public GetAllEmployesQueryTests()
        {
            _repositoryMock = new Mock<IEmployesRepository>();
            _mapper = TestHelper.CreateMapper();
            _query = new GetAllEmployeesQuery(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = new List<Employe>
            {
                TestHelper.CreateTestEmployee(1),
                TestHelper.CreateTestEmployee(2),
                TestHelper.CreateTestEmployee(3)
            };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _query.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnEmptyList_WhenNoEmployeesExist()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Employe>());

            // Act
            var result = await _query.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateAsync_ShouldMapEntitiesToDtos()
        {
            // Arrange
            var employee = TestHelper.CreateTestEmployee(1);
            var employees = new List<Employe> { employee };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _query.ExecuteAsync();
            var firstResult = result.First();

            // Assert
            firstResult.Id.Should().Be(employee.Id);
            firstResult.Email.Should().Be(employee.Email);
            firstResult.Username.Should().Be(employee.Username);
            firstResult.CompanyId.Should().Be(employee.CompanyId);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            // Act & Assert
            Action act = () => new GetAllEmployeesQuery(null!, _mapper);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("repository");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenMapperIsNull()
        {
            // Act & Assert
            Action act = () => new GetAllEmployeesQuery(_repositoryMock.Object, null!);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("mapper");
        }
    }
}