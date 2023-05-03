using Application.Features.Languages.Commands.DeleteLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
    {
        public UpdateTechnologyCommandValidator()
        {
            //Kullanıcının geçersiz bir Id girmesine karşı validationlar
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id alanı boş geçilemez");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id si 0 dan büyük bir değer olmalıdır ");

            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Programlama ismi boşsa güncellenmez");
            RuleFor(x => x.Name).MinimumLength(2);

            RuleFor(x => x.LanguageId).NotEmpty().NotNull().WithMessage("Id alanı boş geçilemez");
            RuleFor(x => x.LanguageId).GreaterThan(0).WithMessage("Id si 0 dan büyük bir değer olmalıdır ");
        }
    }
}
