using FluentValidation;
using RedArbor.Application.DTOs;

namespace RedArbor.Application.Validators
{
    /// <summary>
    /// Validador para CreateEmployeeDto
    /// Valida que todos los campos obligatorios estén presentes y sean correctos
    /// </summary>
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()
        {
            // Validación de CompanyId
            RuleFor(x => x.CompanyId)
                .GreaterThan(0)
                .WithMessage("CompanyId debe ser mayor a 0");

            // Validación de Email
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email es obligatorio")
                .EmailAddress()
                .WithMessage("Email debe ser una dirección válida")
                .MaximumLength(255)
                .WithMessage("Email no puede exceder 255 caracteres");

            // Validación de Password
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password es obligatorio")
                .MaximumLength(255)
                .WithMessage("Password no puede exceder 255 caracteres");

            // Validación de PortalId
            RuleFor(x => x.PortalId)
                .GreaterThan(0)
                .WithMessage("PortalId debe ser mayor a 0");

            // Validación de RoleId
            RuleFor(x => x.RoleId)
                .GreaterThan(0)
                .WithMessage("RoleId debe ser mayor a 0");

            // Validación de StatusId
            RuleFor(x => x.StatusId)
                .GreaterThan(0)
                .WithMessage("StatusId debe ser mayor a 0");

            // Validación de Username
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username es obligatorio")
                .MaximumLength(100)
                .WithMessage("Username no puede exceder 100 caracteres");

            // Validaciones opcionales (solo si tienen valor)
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Name no puede exceder 100 caracteres");

            RuleFor(x => x.Fax)
                .MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.Fax))
                .WithMessage("Fax no puede exceder 50 caracteres");

            RuleFor(x => x.Telephone)
                .MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.Telephone))
                .WithMessage("Telephone no puede exceder 50 caracteres");
        }
    }
}
