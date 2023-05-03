using Application.Features.Languages.Commands.CreateLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
    {
        public CreateTechnologyCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Teknoloji ismi boş geçilemez");
            RuleFor(c => c.Name).MinimumLength(2);

            RuleFor(c => c.LanguageName).NotEmpty().NotNull().WithMessage("Programlama ismi boş geçilemez");
            RuleFor(c => c.LanguageName).MinimumLength(2);

        }
    }
}
