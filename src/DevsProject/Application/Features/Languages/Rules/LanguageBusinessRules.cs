using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Rules
{
    public class LanguageBusinessRules
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageBusinessRules(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        //ProgramlamaDili ismi tekrar edemez
        public async Task LanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            //Kuraldan geçiyorsa hiç birşey döndermiyor  //Kuraldan geçmiyorsa hata fırlatıyor
            //Eğer result dolu gelirse Any() true gelir.Hata fırlatırız

            IPaginate<Language> result = await _languageRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) 
                 throw new BusinessException("Bu Programlama dili bulunmaktadır.Programlamadili adı tekrar edemez");
        }

        //Programlama dili varlığının boş olup olmadığını kontrol eder
        //Null Check      
        public void LanguageShouldExistWhenRequested(Language language)
        {
            if (language is null)
                throw new BusinessException("İstenen Programlama dili mevcut değil");
        }
    }
}
