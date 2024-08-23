using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Constants;
using Reneee.Application.DTOs.Sales;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(ISalesService salesService) : ControllerBase
    {
        private readonly ISalesService _salesService = salesService;

        [HttpGet("reports/monthly-orders")]
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<MonthlyOrderCountReportDto>> GetMonthlyOrderCountReport()
        {
            return Ok(await _salesService.GetMonthlyOrderCountReport());
        }
    }
}
