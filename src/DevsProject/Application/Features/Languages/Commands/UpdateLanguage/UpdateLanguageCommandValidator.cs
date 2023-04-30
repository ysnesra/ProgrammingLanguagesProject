using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator :AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            //Kullanıcının geçersiz bir Id girmesine karşı validationlar
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id alanı boş geçilemez");
            RuleFor(p => p.Id).GreaterThan(0).WithMessage("Id si 0 dan büyük bir değer olmalıdır ");

            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Programlama ismi boşsa güncellenmez");
            RuleFor(c => c.Name).MinimumLength(2);
        }
    }
}
