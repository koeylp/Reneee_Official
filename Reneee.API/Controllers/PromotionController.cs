using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Promotion;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController(IPromotionService promotionService) : ControllerBase
    {
        private readonly IPromotionService _promotionService = promotionService;

        [HttpPost]
        public async Task<ActionResult<PromotionDto>> CreatePromotion([FromBody] CreatePromotionDto promotionRequest)
        {
            return Ok(await _promotionService.CreatePromotion(promotionRequest));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PromotionDto>>> GetAllPromotions()
        {
            return Ok(await _promotionService.GetAllPromotions());
        }
    }
}
