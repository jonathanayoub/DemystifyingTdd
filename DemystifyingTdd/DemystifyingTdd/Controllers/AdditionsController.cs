using DemystifyingTdd.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DemystifyingTdd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(Addition additionData)
        {
            return Ok(additionData.Numbers.Sum());
        }
    }
}
