using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Constants;
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
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<AttributeDto>> CreateAttribute([FromBody] CreateUpdateAttributeDto attributeRequest)
        {
            return Ok(await _attributeService.CreateAttribute(attributeRequest));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AttributeDto>>> GetAllAttributes()
        {
            return Ok(await _attributeService.GetAllAttributes());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAttribute(int id)
        {
            return Ok(await _attributeService.DeleteAttribute(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AttributeDto>> UpdateAttibute([FromRoute] int id, [FromBody] CreateUpdateAttributeDto attributeRequest)
        {
            return Ok(await _attributeService.UpdateAttibute(id, attributeRequest));
        }
    }
}
