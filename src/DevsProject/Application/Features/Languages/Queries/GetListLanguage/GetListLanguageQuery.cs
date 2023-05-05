using Application.Features.Languages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Queries.GetListLanguage
{
    /// <summary>
    /// Programlama dili Listelemek için sorgu sınıfı
    /// </summary>
    public class GetListLanguageQuery: IRequest<LanguageListModel>
    {
        public PageRequest PageRequest { get; set; }   

        /// <summary>
        /// Programlama Dili Listelemek için kullanılan işleyici sınıfıdır.
        /// </summary>
        public class GetListLanguageQueryHandler : IRequestHandler<GetListLanguageQuery,LanguageListModel>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;

            public GetListLanguageQueryHandler(ILanguageRepository languageRepository, IMapper mapper)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
            }

            public async Task<LanguageListModel> Handle(GetListLanguageQuery request, CancellationToken cancellationToken)
            {
               IPaginate<Language> languages = await _languageRepository.GetListAsync(
                   include:m=>m.Include(x=>x.Technologies),
                   index: request.PageRequest.Page, size: request.PageRequest.PageSize);  
                
                LanguageListModel mappedLanguageListModel = _mapper.Map<LanguageListModel>(languages);

                return mappedLanguageListModel;
            }
        }
    }
}
