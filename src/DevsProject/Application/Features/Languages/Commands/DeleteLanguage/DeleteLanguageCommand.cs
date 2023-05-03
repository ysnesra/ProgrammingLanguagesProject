using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    /// <summary>
    /// Programlama Dili silmek için kullanılan komut sınıfıdır.
    /// </summary>
    public class DeleteLanguageCommand : IRequest<DeletedLanguageDto>
    {
        public string Name { get; set; }

        /// <summary>
        /// Programlama Dili silmek için kullanılan işleyici sınıfıdır.
        /// </summary>
        public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, DeletedLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<DeletedLanguageDto> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {
                //BusinessRules
                Language? language = await _languageRepository.GetAsync(x => x.Name.ToLower() == request.Name.ToLower());

                _languageBusinessRules.LanguageShouldExistWhenRequested(language);

                Language deletedLanguage= await _languageRepository.DeleteAsync(language);
                DeletedLanguageDto deletedLanguageDto = _mapper.Map<DeletedLanguageDto>(deletedLanguage);

                return deletedLanguageDto;
            }
        }
    }
}
