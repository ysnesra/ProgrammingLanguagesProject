using Application.Features.Languages.Commands.DeleteLanguage;
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

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    /// <summary>
    /// Teknolojiyi silmek için kullanılan komut sınıfıdır.
    /// </summary>
    public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
    {
        public string Name { get; set; }

        /// <summary>
        /// Teknolojiyi silmek için kullanılan işleyici sınıfıdır.
        /// </summary>
        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology? technology= await _technologyRepository.GetAsync(x=>x.Name.ToLower()==request.Name.ToLower());
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

                Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);
                DeletedTechnologyDto deletedTechnologyDto= _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
                return deletedTechnologyDto;
            }
        }
    }
}
