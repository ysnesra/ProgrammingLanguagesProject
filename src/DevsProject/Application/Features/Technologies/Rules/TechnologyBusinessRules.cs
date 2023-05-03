using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly ILanguageRepository _languageRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository, ILanguageRepository languageRepository)
        {
            _technologyRepository = technologyRepository;
            _languageRepository = languageRepository;
        }

      
        //Programlama dili olup olmadığını kontrol eder
        //Null Check
        public async Task LanguageMustExit(int languageId)
        {
            Language language = await _languageRepository.GetAsync(x => x.Id == languageId);
            if (language is null)
                throw new BusinessException("İstenen Programlama dili mevcut değil");
        }
        //Programlama dilini isminden olup olmadığını kontrol eder
        //Null Check
        public async Task LanguageNameMustExit(string languageName)
        {
            Language language = await _languageRepository.GetAsync(x => x.Name == languageName);
            if (language is null)
                throw new BusinessException("İstenen Programlama dili mevcut değil");
        }
        

        //Teknoloji ismi tekrar edemez
        public async Task TechnologyNameCanNotBeDuplicated(string name)
        {
            //Kuraldan geçiyorsa hiç birşey döndermiyor  //Kuraldan geçmiyorsa hata fırlatıyor
            //Eğer result dolu gelirse Any() true gelir.Hata fırlatır

            IPaginate<Technology> result = await _technologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any())
                throw new BusinessException("Bu Teknoloji bulunmaktadır.Teknoloji adı tekrar edemez");
        }

        //Teknoloji varlığının boş olup olmadığını kontrol eder
        //Null Check      
        public void TechnologyShouldExistWhenRequested(Technology technology)
        {
            if (technology is null)
                throw new BusinessException("İstenen Teknoloji mevcut değil");
        }

    }
}
