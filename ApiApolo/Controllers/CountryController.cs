using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApolo.Dtos;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiApolo.Controllers
{
    public class CountryController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> Get()
        {
            var countries = await _unitOfWork.Countries.getCountriesAndStates();
            return _mapper.Map<List<CountryDto>>(countries);
        }
        [HttpGet("getCountriesAndStatesAndCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCoutriesStateCities()
        {
            var countries = await _unitOfWork.Countries.getCountriesAndStatesAndCities();
            return _mapper.Map<List<CountryDto>>(countries);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryDto>> Get(int id)
        {
            var countries = await _unitOfWork.Countries.GetByIdAsync(id);
            if(countries == null)
            {
                return BadRequest();
            }
            return _mapper.Map<CountryDto>(countries);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryDto>> Post([FromBody] CountryDto countryDto)
        {
            
            var countries = _mapper.Map<Country>(countryDto);
            _unitOfWork.Countries.Add(countries);
            await _unitOfWork.SaveAsync();
            if(countries == null)
            {
                return BadRequest();
            }
            countryDto.Id = countries.Id;
            return CreatedAtAction(nameof(Post), new {id = countryDto.Id}, countryDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryDto>> Put([FromBody] CountryDto countryDto, int id)
        {
            if(countryDto ==null)
            {
                return NotFound();
            }
            var countries = _unitOfWork.Countries.GetByIdAsync(id);            
            if(countries == null)
            {
                return NotFound();
            }
            var countriesM = _mapper.Map<Country>(countryDto);
            _unitOfWork.Countries.Update(countriesM);
            await _unitOfWork.SaveAsync();
            return countryDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var countries = await _unitOfWork.Countries.GetByIdAsync(id);            
            if(countries ==null)
            {
                return NotFound();
            }
    
            _unitOfWork.Countries.Remove(countries);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}