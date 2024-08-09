using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Contracts.ThirdService;

namespace Reneee.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GhnController(IGhnService ghnService) : ControllerBase
    {
        private readonly IGhnService _ghnService = ghnService;

        [HttpGet("provinces")]
        public async Task<ActionResult> GetProvinces()
        {
            var provinces = await _ghnService.GetProvinces();
            return Ok(provinces);
        }

        [HttpGet("district/{provinceId}")]
        public async Task<ActionResult> GetDisctrictByPronvinceId([FromRoute] int provinceId)
        {
            var districts = await _ghnService.GetDisctrictByPronvinceId(provinceId);
            return Ok(districts);
        }

        [HttpGet("ward")]
        public async Task<ActionResult> GetWardByDistrictId([FromQuery] int districtId)
        {
            var wards = await _ghnService.GetWardByDistrictId(districtId);
            return Ok(wards);
        }
    }
}
