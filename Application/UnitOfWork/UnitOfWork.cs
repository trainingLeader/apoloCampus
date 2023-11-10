using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApoloCampusContext _context;

        public ICity _cities;

        public ICountry _countries;

        public IState _states;

        public ICustomer _customers;

        public IPersonType _personsTypes;
        public UnitOfWork(ApoloCampusContext context)
        {
            _context = context;
        }
        public ICity Cities
        {
            get
            {
                _cities ??= new CityRepository(_context);
                return _cities;
            }
        }

        public ICountry Countries
        {
            get
            {
                _countries ??= new CountryRepository(_context);
                return _countries;
            }
        }
        public ICustomer Customers
        {
            get
            {
                _customers ??= new CustomerRepository(_context);
                return _customers;
            }
        }
        public IPersonType PersonsTypes
        {
            get
            {
                _personsTypes ??= new PersonTypeRepository(_context);
                return _personsTypes;
            }
        }
        public IState States
        {
            get
            {
                _states ??= new StateRepository(_context);
                return _states;
            }
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}