using Application.Features.Languages.Dtos;
using Application.Features.Languages.Queries.GetByIdLanguage;
using Application.Features.Languages.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetByIdTechnology
{
    /// <summary>
    /// Tek bir Teknolojinin detayını Programlama diliyle getiren sorgu sınıfıdır
    /// </summary>
    public class GetByIdTechnologyQuery : IRequest<TechnologyGetByIdDto>
    {
        public int Id { get; set; }

        /// <summary>
        /// Tek bir Teknolojiyi Programlama Diliyle getiren işleyici sınıfıdır.
        /// </summary>
        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyGetByIdDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<TechnologyGetByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
            {
                Technology? technology = await _technologyRepository.Query().Include(x => x.Language).FirstOrDefaultAsync(x => x.Id == request.Id);

                //Teknolojinin olup olmadığını kontrol eden iş kuralı
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

          
                TechnologyGetByIdDto technologyGetByIdDto = _mapper.Map<TechnologyGetByIdDto>(technology);
                return technologyGetByIdDto;
            }
        }
    }
}
