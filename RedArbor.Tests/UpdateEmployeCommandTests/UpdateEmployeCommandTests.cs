using AutoMapper;
using FluentAssertions;
using Moq;
using RedArbor.Application.Commands;
using RedArbor.Domain.Entities;
using RedArbor.Domain.Interface;
using RedArbor.Tests.Helpers;

namespace RedArbor.Tests.CommandsTests
{
    /// <summary>
    /// Tests para UpdateEmployeeCommand
    /// Verifica que el Metodo de actualización funcione correctamente
    /// </summary>
    public class UpdateEmployeCommandTests
    {
        private readonly Mock<IEmployesRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly UpdateEmployeeCommand _command;

        public UpdateEmployeCommandTests()
        {
            _repositoryMock = new Mock<IEmployesRepository>();
            _mapper = TestHelper.CreateMapper();
            _command = new UpdateEmployeeCommand(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldUpdateEmployee_WhenDataIsValid()
        {
            // Arrange


            var updateDto = TestHelper.CreateTestUpdateEmployeeDto();
            var employeeId = 1;

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Employe>()))
                .ReturnsAsync(true);

            // Act
            var result = await _command.ExecuteAsync(employeeId, updateDto);

            // Assert
            result.Should().BeTrue();
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Employe>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var updateDto = TestHelper.CreateTestUpdateEmployeeDto();
            var employeeId = 999;

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Employe>()))
                .ReturnsAsync(false);

            // Act
            var result = await _command.ExecuteAsync(employeeId, updateDto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_ShouldAssignIdAndMapFields()
        {
            // Arrange
            var updateDto = TestHelper.CreateTestUpdateEmployeeDto();
            var employeeId = 42;
            Employe capturedEmployee = null!;

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Employe>()))
                .Callback<Employe>(emp => capturedEmployee = emp)
                .ReturnsAsync(true);

            // Act
            await _command.ExecuteAsync(employeeId, updateDto);

            // Assert
            capturedEmployee.Should().NotBeNull();
            capturedEmployee.Id.Should().Be(employeeId);
            capturedEmployee.Email.Should().Be(updateDto.Email);
            capturedEmployee.Username.Should().Be(updateDto.Username);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            // Act & Assert
            Action act = () => new UpdateEmployeeCommand(null!, _mapper);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("repository");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenMapperIsNull()
        {
            // Act & Assert
            Action act = () => new UpdateEmployeeCommand(_repositoryMock.Object, null!);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("mapper");
        }
    }
}