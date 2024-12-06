using FluentValidation;
using StudentClassManagement.Core.DTOs;

namespace StudentClassManagement.Core.Validation;

public class ClassValidator: AbstractValidator<ClassDto>
{
    public ClassValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters.");
    }
}