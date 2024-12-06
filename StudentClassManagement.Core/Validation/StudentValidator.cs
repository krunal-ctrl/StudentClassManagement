using FluentValidation;
using StudentClassManagement.Core.DTOs;

namespace StudentClassManagement.Core.Validation;

public class StudentValidator: AbstractValidator<StudentDto>
{
    public StudentValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.")
            .NotEmpty();
        RuleFor(x => x.EmailId)
            .EmailAddress().WithMessage("Invalid email format.")
            .NotEmpty();
    }
}