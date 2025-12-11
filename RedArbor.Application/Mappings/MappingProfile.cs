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
    /// Configura todos los mapeos entre Entidades y DTOs
    /// </summary>
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
           
            CreateMap<Employe, EmployesDto>();
            CreateMap<CreateEmployeeDto, Employe>();
            CreateMap<UpdateEmployeeDto, Employe>();
        }
    }
}
