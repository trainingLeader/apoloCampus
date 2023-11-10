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

    public class CustomerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var productos = await _unitOfWork.Customers
                                        .GetAllAsync();

            return _mapper.Map<List<CustomerDto>>(productos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> Get(int id)
        {
            var producto = await _unitOfWork.Customers.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            return _mapper.Map<CustomerDto>(producto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> Post(CustomerDto customerDto)
        {
            var producto = _mapper.Map<Customer>(customerDto);
            _unitOfWork.Customers.Add(producto);
            await _unitOfWork.SaveAsync();
            if (producto == null)
            {
                return BadRequest();
            }
            customerDto.Id = producto.Id;
            return CreatedAtAction(nameof(Post), new { id = customerDto.Id }, customerDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> Put(int id, [FromBody] CustomerDto productoDto)
        {
            if (productoDto == null)
                return NotFound();

            var productoBd = await _unitOfWork.Customers.GetByIdAsync(id);
            if (productoBd == null)
                return NotFound();

            var producto = _mapper.Map<Customer>(productoDto);
            _unitOfWork.Customers.Update(producto);
            await _unitOfWork.SaveAsync();
            return productoDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unitOfWork.Customers.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            _unitOfWork.Customers.Remove(producto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}