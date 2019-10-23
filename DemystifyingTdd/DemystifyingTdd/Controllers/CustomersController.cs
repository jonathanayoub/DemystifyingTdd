using Microsoft.AspNetCore.Mvc;

namespace DemystifyingTdd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public IActionResult GetCustomers()
        {
            return Ok(0);
        }
    }
}
