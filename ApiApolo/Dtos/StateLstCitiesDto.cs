using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApolo.Dtos
{
    public class StateLstCitiesDto
    {
        public string Name { get;set;}
        public List<CityDto> Cities { get;set;}
    }
}