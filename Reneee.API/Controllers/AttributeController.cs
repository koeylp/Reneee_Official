using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Attribute;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController(IAttributeService attributeService) : ControllerBase
    {
        private readonly IAttributeService _attributeService = attributeService;

        [HttpPost]
        public async Task<ActionResult<AttributeDto>> CreateAttribute([FromBody] CreateUpdateAttributeDto attributeRequest)
        {
            return Ok(await _attributeService.CreateAttribute(attributeRequest));
        }
    }
}
