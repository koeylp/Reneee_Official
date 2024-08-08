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

        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionDto>> GetPromotionById([FromRoute] int id)
        {
            return Ok(await _promotionService.GetPromotionById(id));
        }

        [HttpPut("disable/{id}")]
        public async Task<ActionResult<PromotionDto>> DisablePromotion([FromRoute] int id)
        {
            return Ok(await _promotionService.DisablePromotion(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePromotion([FromRoute] int id)
        {
            return Ok(await _promotionService.DeletePromotion(id));
        }

        
    }
}
