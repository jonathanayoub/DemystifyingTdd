using DemystifyingTdd.Api.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemystifyingTdd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersHandler _customersHandler;

        public CustomersController(ICustomersHandler customersHandler)
        {
            _customersHandler = customersHandler
                                ?? throw new ArgumentNullException(nameof(customersHandler));
        }

        public IActionResult GetCustomers()
        {
            var customerList = _customersHandler.GetAll();
            return Ok(customerList);
        }
    }
}
