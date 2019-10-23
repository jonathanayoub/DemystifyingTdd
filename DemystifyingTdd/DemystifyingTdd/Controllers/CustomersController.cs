using DemystifyingTdd.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemystifyingTdd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public IActionResult GetCustomers()
        {
            return Ok(new List<Customer>
            {
                new Customer(),
                new Customer(),
                new Customer(),
                new Customer(),
                new Customer()
            });
        }
    }
}
