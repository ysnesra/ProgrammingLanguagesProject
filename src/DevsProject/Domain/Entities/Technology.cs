using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Technology: Entity
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public Technology()
        {
        }

        public Technology(int id, int languageId,string name):this()
        {
            Id = id;
            LanguageId = languageId;
            Name = name;
                   
        }
    }
}
