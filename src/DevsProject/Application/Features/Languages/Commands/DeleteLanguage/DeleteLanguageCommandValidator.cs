using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandValidator : AbstractValidator<DeleteLanguageCommand>
    {
        public DeleteLanguageCommandValidator()
        {
            //Kullanıcının geçersiz bir Id girmesine karşı validationlar
            RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("Programlama ismi boşsa silinmez");
            RuleFor(p => p.Id).GreaterThan(0).WithMessage("Id si 0 dan büyük bir değer olmalıdır ");
        }
       
    }
}
