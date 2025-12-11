using AutoMapper;
using RedArbor.Application.DTOs;
using RedArbor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Application.Mappings
{
    /// <summary>
    /// Perfil de mapeo para AutoMapper
    /// Configura todos los mapeos entre Entidades y DTOs
    /// </summary>
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Mapeo de Employe (Entity) a EmployeeDto (Response)
            CreateMap<Employe, EmployesDto>();

            // Mapeo de CreateEmployeeDto (Request) a Employee (Entity)
            CreateMap<CreateEmployeeDto, Employe>();

            // Mapeo de UpdateEmployeeDto (Request) a Employee (Entity)
            CreateMap<UpdateEmployeeDto, Employe>();
        }
    }
}
