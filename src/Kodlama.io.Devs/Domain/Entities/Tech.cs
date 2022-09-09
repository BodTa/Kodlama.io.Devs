using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tech:Entity
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public virtual ProgrammingLanguage? Language { get; set; }

        public Tech()
        {

        }
        public Tech(int id,string name,int languageId)
        {
            LanguageId = languageId;
            Name = name;
            Id = id;
        }
    }
}
