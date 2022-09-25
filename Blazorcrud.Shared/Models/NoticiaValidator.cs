using FluentValidation;

namespace Blazorcrud.Shared.Models
{
    public class NoticiaValidator : AbstractValidator<Noticia>
    {
        public NoticiaValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(person => person.Titulo).NotEmpty().WithMessage("First name is a required field.")
                .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.");
            RuleFor(person => person.Body).NotEmpty().WithMessage("Last name is a required field.")
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.");

            RuleFor(person => person.IdCategoria).NotEmpty().WithMessage("Phone number is a required field.")
                .Length(5, 50).WithMessage("Phone number must be between 5 and 50 characters.");

        }
    }
}