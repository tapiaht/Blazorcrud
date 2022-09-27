using FluentValidation;

namespace Blazorcrud.Shared.Models
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(upload => upload.Nombre).NotEmpty().WithMessage("File name is a required field.")
                .Length(5, 50).WithMessage("File name must be between 5 and 50 characters.");
            //RuleFor(upload => upload.Foto).NotEmpty().WithMessage("Uploaded file is required.");
        }
    }
}