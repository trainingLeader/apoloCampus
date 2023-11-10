using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiApolo.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiApolo.Controllers
{
    
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CityDto>>> Get()
        {
            var productos = await _unitOfWork.Cities
                                        .GetAllAsync();

            return _mapper.Map<List<CityDto>>(productos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var producto = await _unitOfWork.Cities.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            return _mapper.Map<CityDto>(producto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<City>> Post(CityDto cityDto)
        {
            var producto = _mapper.Map<City>(cityDto);
            _unitOfWork.Cities.Add(producto);
            await _unitOfWork.SaveAsync();
            if (producto == null)
            {
                return BadRequest();
            }
            cityDto.Id = producto.Id;
            return CreatedAtAction(nameof(Post), new { id = cityDto.Id }, cityDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityDto>> Put(int id, [FromBody] CityDto productoDto)
        {
            if (productoDto == null)
                return NotFound();

            var productoBd = await _unitOfWork.Cities.GetByIdAsync(id);
            if (productoBd == null)
                return NotFound();

            var producto = _mapper.Map<City>(productoDto);
            _unitOfWork.Cities.Update(producto);
            await _unitOfWork.SaveAsync();
            return productoDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unitOfWork.Cities.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            _unitOfWork.Cities.Remove(producto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}