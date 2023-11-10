using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICity Cities{get;}
        ICountry Countries{get;}
        IState States{get;}
        ICustomer Customers{get;}
        IPersonType PersonsTypes{get;}
        Task<int> SaveAsync();
    }
}