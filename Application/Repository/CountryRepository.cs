using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountry
    {
        private readonly ApoloCampusContext _context;
        public CountryRepository(ApoloCampusContext context) : base(context)
        {
            _context=context;
        }
        /* public async Task<IEnumerable<Country>>GetAll(){
            return await _context.Countries.Include(j=>j.States).ToListAsync();
        } */

        public async Task<IEnumerable<Country>> getCountriesAndStates()
        {
            return await _context.Countries
                .Include(j=>j.States)
                .ToListAsync();
        }


        Task<Country> ICountry.GetCountriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Country>> getCountriesAndStatesAndCities()
        {
                return await _context.Countries
                .Include(j=>j.States)
                .ThenInclude(c => c.Cities)
                .ToListAsync();
        }
    }
}