using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetListLanguage;
using Application.Features.Technologies.Models;
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

namespace Application.Features.Technologies.Queries.GetListTechnology
{
    /// <summary>
    /// Teknolojileri Listelemek için sorgu sınıfı
    /// </summary>
    public class GetListTechnologyQuery : IRequest<TechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }

        /// <summary>
        /// Teknolojileri Listelemek için kullanılan işleyici sınıfıdır.
        /// </summary>
        public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetListTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }
            public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
            {
                //Technoloy ve Language tablosu joinleme
                IPaginate<Technology> technologies = await _technologyRepository.GetListAsync(include:
                    m => m.Include(x => x.Language),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );

                TechnologyListModel mappedTechnologyListModel = _mapper.Map<TechnologyListModel>(technologies);

                return mappedTechnologyListModel;
            }   
        }
        
    }
}
