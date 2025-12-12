using FluentAssertions;
using Moq;
using RedArbor.Application.Commands;
using RedArbor.Domain.Interface;

namespace RedArbor.Tests.DeleteEmployeCommandTests
{
    /// <summary>
    /// Tests para DeleteEmployeeCommand
    /// Verifica que el comando de eliminación funcione correctamente
    /// </summary>
    public class DeleteEmployeCommandTests
    {
        private readonly Mock<IEmployesRepository> _repositoryMock;
        private readonly DeleteEmployeeCommand _command;

        public DeleteEmployeCommandTests()
        {
            _repositoryMock = new Mock<IEmployesRepository>();
            _command = new DeleteEmployeeCommand(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnTrue_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = 1;
            _repositoryMock
                .Setup(r => r.DeleteAsync(employeeId))
                .ReturnsAsync(true);

            // Act
            var result = await _command.ExecuteAsync(employeeId);

            // Assert
            result.Should().BeTrue();
            _repositoryMock.Verify(r => r.DeleteAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = 999;
            _repositoryMock
                .Setup(r => r.DeleteAsync(employeeId))
                .ReturnsAsync(false);

            // Act
            var result = await _command.ExecuteAsync(employeeId);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_ShouldInvokeRepositoryWithCorrectId()
        {
            // Arrange
            var employeeId = 42;
            _repositoryMock
                .Setup(r => r.DeleteAsync(employeeId))
                .ReturnsAsync(true);

            // Act
            await _command.ExecuteAsync(employeeId);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(employeeId), Times.Once);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            // Act & Assert
            Action act = () => new DeleteEmployeeCommand(null!);
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("repository");
        }
    }
}