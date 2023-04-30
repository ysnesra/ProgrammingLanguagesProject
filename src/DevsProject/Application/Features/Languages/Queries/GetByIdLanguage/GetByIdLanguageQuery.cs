using Application.Features.Languages.Dtos;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetListLanguage;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Queries.GetByIdLanguage
{
    /// <summary>
    /// Tek bir Programlama dili detayını getiren sorgu sınıfıdır
    /// </summary>
    public class GetByIdLanguageQuery: IRequest<LanguageGetByIdDto>
    {
        public int Id { get; set; }

        /// <summary>
        /// Tek bir Programlama dilini getiren işleyici sınıfıdır.
        /// </summary>
        public class GetByIdLanguageQueryHandler : IRequestHandler<GetByIdLanguageQuery, LanguageGetByIdDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public GetByIdLanguageQueryHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<LanguageGetByIdDto> Handle(GetByIdLanguageQuery request, CancellationToken cancellationToken)
            {
                Language? language= await _languageRepository.GetAsync(x => x.Id == request.Id);

                //Programlama dilinin olup olmadığını kontrol eden iş kuralı
                _languageBusinessRules.LanguageShouldExistWhenRequested(language);  

                LanguageGetByIdDto languageGetByIdDto =_mapper.Map<LanguageGetByIdDto>(language);   
                return languageGetByIdDto;
            }
        }
    }
}
