using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    /// <summary>
    /// Teknoloji güncellemek için kullanılan komut sınıfıdır.
    /// </summary>
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }    
        
       
    }
    /// <summary>
    /// Teknoloji güncellemek için kullanılan işleyici sınıfıdır.
    /// </summary>
    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;       
        private readonly IMapper _mapper;    
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;          
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            //BusinessRules ->
            //Teknoloji ismi tekrar edemez
            //LanguageId si nullcheck kont yap.Programlama dili olup olmadığını kontrol eder
            //Girilen Id de teknoloji var mı
            //Varsa; Teknoloji varlığının boş olup olmadığını kontrol eder


            await _technologyBusinessRules.TechnologyNameCanNotBeDuplicated(request.Name);
            await _technologyBusinessRules.LanguageMustExit(request.LanguageId);
            Technology technology = await _technologyRepository.GetAsync(x => x.Id == request.Id);        
            _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

            technology.Name = request.Name;
           
            Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
            UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);
            return updatedTechnologyDto;
        }
    }
}
