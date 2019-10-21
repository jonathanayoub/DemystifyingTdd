using DemystifyingTdd.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemystifyingTdd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(Addition additionData)
        {
            return Ok(additionData.Number1 + additionData.Number2);
        }
    }
}
