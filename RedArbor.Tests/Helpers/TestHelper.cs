using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Application.Mappings;
using RedArbor.Domain.Entities;

namespace RedArbor.Tests.Helpers
{
    /// <summary>
    /// Clase helper con métodos utilitarios para los tests
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Crea una instancia de IMapper configurada con el MappingProfile
        /// </summary>
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return configuration.CreateMapper();
        }

        /// <summary>
        /// Crea un Employee de prueba con valores por defecto
        /// </summary>
        public static Employe CreateTestEmployee(int id = 1)
        {
            return new Employe
            {
                Id = id,
                CompanyId = 1,
                Email = $"test{id}@test.com",
                Password = "test123",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Username = $"testuser{id}",
                Name = $"Test User {id}",
                Fax = "123-456-789",
                Telephone = "987-654-321",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                DeleteOn = null,
                Lastlogin = DateTime.Now
            };
        }

        /// <summary>
        /// Crea un CreateEmployeeDto de prueba
        /// </summary>
        public static CreateEmployeeDto CreateTestEmployeeDto()
        {
            return new CreateEmployeeDto
            {
                CompanyId = 1,
                Email = "newemployee@test.com",
                Password = "password123",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Username = "newemployee",
                Name = "New Employee",
                Fax = "111-222-333",
                Telephone = "444-555-666",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
        }

        /// <summary>
        /// Crea un UpdateEmployeeDto de prueba
        /// </summary>
        public static UpdateEmployeeDto CreateTestUpdateEmployeeDto()
        {
            return new UpdateEmployeeDto
            {
                CompanyId = 1,
                Email = "updated@test.com",
                Password = "updatedpass",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Username = "updateduser",
                Name = "Updated Name",
                Fax = "999-888-777",
                Telephone = "666-555-444",
                UpdatedOn = DateTime.Now
            };
        }
    }
}