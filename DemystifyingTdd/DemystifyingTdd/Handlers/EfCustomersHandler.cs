using AutoMapper;
using DemystifyingTdd.Api.Data;
using DemystifyingTdd.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DemystifyingTdd.Api.Handlers
{
    public class EfCustomersHandler : ICustomersHandler
    {
        private readonly TddContext _dbContext;
        private readonly IMapper _mapper;

        public EfCustomersHandler(TddContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext
                         ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper
                      ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IList<Customer> GetAll()
        {
            var customerEntities = _dbContext.Customers.Include(c => c.Subscriptions);

            var mappedApiCustomers = _mapper.Map<IList<Customer>>(customerEntities);

            return mappedApiCustomers;
        }
    }
}
