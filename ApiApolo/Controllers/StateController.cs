using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApolo.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiApolo.Controllers
{
    public class StateController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StateController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StateDto>>> Get()
        {
            var productos = await _unitOfWork.States
                                        .GetAllAsync();

            return _mapper.Map<List<StateDto>>(productos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StateDto>> Get(int id)
        {
            var producto = await _unitOfWork.States.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            return _mapper.Map<StateDto>(producto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<State>> Post(StateDto StateDto)
        {
            var producto = _mapper.Map<State>(StateDto);
            _unitOfWork.States.Add(producto);
            await _unitOfWork.SaveAsync();
            if (producto == null)
            {
                return BadRequest();
            }
            StateDto.Id = producto.Id;
            return CreatedAtAction(nameof(Post), new { id = StateDto.Id }, StateDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StateDto>> Put(int id, [FromBody] StateDto productoDto)
        {
            if (productoDto == null)
                return NotFound();

            var productoBd = await _unitOfWork.States.GetByIdAsync(id);
            if (productoBd == null)
                return NotFound();

            var producto = _mapper.Map<State>(productoDto);
            _unitOfWork.States.Update(producto);
            await _unitOfWork.SaveAsync();
            return productoDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unitOfWork.States.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            _unitOfWork.States.Remove(producto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}