﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Constants;
using Reneee.Application.DTOs.AttributeValue;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeValueController(IAttributeValueService attributeValueService) : ControllerBase
    {
        private readonly IAttributeValueService _attributeValueService = attributeValueService;

        [HttpPost]
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<AttributeValueDto>> CreateAttributeValue([FromBody] CreateAttributeValueDto attributeValueRequest)
        {
            return Ok(await _attributeValueService.CreateAttributeValue(attributeValueRequest));
        }
    }
}
