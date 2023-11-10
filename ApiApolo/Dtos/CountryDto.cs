using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApolo.Dtos
{
    public class CountryDto : BaseDto
    {
        public string Name {get;set;}
        public List<StateLstCitiesDto>States {get;set;}
    }
}