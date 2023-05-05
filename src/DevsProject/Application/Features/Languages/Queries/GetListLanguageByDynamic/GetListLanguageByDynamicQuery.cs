using Application.Features.Languages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Queries.GetListLanguageByDynamic
{
    /// <summary>
    /// Programlama dilini Dinamik Filtreli getirmek için sorgu sınıfı
    /// </summary>
    public class GetListLanguageByDynamicQuery : IRequest<LanguageListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
    }
    /// <summary>
    /// Programlama dilini Dinamik Filtreli getirmek için işleyici sınıfı
    /// </summary>
    public class GetListLanguageByDynamicQueryHandler : IRequestHandler<GetListLanguageByDynamicQuery, LanguageListModel>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public GetListLanguageByDynamicQueryHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<LanguageListModel> Handle(GetListLanguageByDynamicQuery request, CancellationToken cancellationToken)
        {
           IPaginate<Language> languages= await _languageRepository.GetListByDynamicAsync(
                request.Dynamic,
                include: m => m.Include(c => c.Technologies),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize);
            LanguageListModel mappedLanguages = _mapper.Map<LanguageListModel>(languages);
            return mappedLanguages;

        }
    }
}
