using Application.Features.Technologies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Dtos
{
    public class LanguageGetByIdDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public ICollection<TechnologyListDto> Technologies { get; set; }    
    }
}
