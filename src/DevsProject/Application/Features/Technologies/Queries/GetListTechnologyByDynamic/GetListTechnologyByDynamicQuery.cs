using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetListTechnology;
using Application.Services.Repositories;
using AutoMapper;
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

namespace Application.Features.Technologies.Queries.GetListTechnologyByDynamic
{
    public class GetListTechnologyByDynamicQuery : IRequest<TechnologyListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
        public class GetListTechnologyByDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, TechnologyListModel>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetListTechnologyByDynamicQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
            {
                //Technoloy ve Language tablosu joinleme
                IPaginate<Technology> technologies = await _technologyRepository.GetListByDynamicAsync(request.Dynamic,include:
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
