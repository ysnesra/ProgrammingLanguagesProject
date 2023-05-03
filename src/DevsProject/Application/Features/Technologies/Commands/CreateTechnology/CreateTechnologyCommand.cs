using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    /// <summary>
    /// Teknoloji eklemek için kullanılan komut sınıfıdır.
    /// </summary>
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public string LanguageName { get; set; }

        /// <summary>
        /// Teknoloji eklemek için kullanılan işleyici sınıfıdır.
        /// </summary>
        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules,ILanguageRepository languageRepository)
            {
                _technologyRepository = technologyRepository;
                _languageRepository = languageRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicated(request.Name);
                await _technologyBusinessRules.LanguageNameMustExit(request.LanguageName);

                Language language = await _languageRepository.GetAsync(x => x.Name == request.LanguageName);

                Technology mappedTechnology = _mapper.Map<Technology>(request);
                mappedTechnology.LanguageId = language.Id;
                Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
                CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);

                return createdTechnologyDto;
            }
        }
    }
}
