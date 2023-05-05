using Application.Features.Languages.Commands.DeleteLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommandValidator : AbstractValidator<DeleteTechnologyCommand>
    {
        public DeleteTechnologyCommandValidator()
        {
           RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("Teknoloji Id si boşsa silinmez");
            RuleFor(d => d.Id).GreaterThan(0).WithMessage("Tenoloji Id si negatif olmaz");
        }
    }
}
