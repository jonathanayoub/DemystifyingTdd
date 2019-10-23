using DemystifyingTdd.Api.Models;
using System.Collections.Generic;

namespace DemystifyingTdd.Api.Handlers
{
    public interface ICustomersHandler
    {
        IList<Customer> GetAll();
    }
}
