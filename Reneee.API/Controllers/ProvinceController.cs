using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.District;
using Reneee.Application.DTOs.Province;
using Reneee.Application.DTOs.Ward;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController(IProvinceService provinceService) : ControllerBase
    {
        private readonly IProvinceService _provinceService = provinceService;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProvinceDto>>> GetAllProvinces()
        {
            return Ok(await _provinceService.GetAllProvinces());
        }

        [HttpGet("district/{provinceCode}")]
        public async Task<ActionResult<IReadOnlyList<DistrictDto>>> GetDistrictsByProvince([FromRoute] string provinceCode)
        {
            return Ok(await _provinceService.GetDistrictsByProvince(provinceCode));
        }

        [HttpGet("ward/{districtCode}")]
        public async Task<ActionResult<IReadOnlyList<WardDto>>> GetWardsByDistrict([FromRoute] string districtCode)
        {
            return Ok(await _provinceService.GetWardsByDistrict(districtCode));
        }
    }
}
