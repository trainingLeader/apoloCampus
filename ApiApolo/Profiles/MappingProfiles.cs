using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApolo.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiApolo.Profiles 
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<City ,CityDto>().ReverseMap();
            CreateMap<State,StateLstCitiesDto>().ReverseMap();
            CreateMap<State ,StateDto>().ReverseMap();
            CreateMap<Country ,CountryDto>().ReverseMap();
            CreateMap<Customer ,CustomerDto>().ReverseMap();
        }
        
    }
}