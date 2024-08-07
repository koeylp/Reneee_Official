using Reneee.Application.DTOs.Promotion;

namespace Reneee.Application.Services
{
    public interface IPromotionService
    {
        Task<PromotionDto> CreatePromotion(CreatePromotionDto promotionRequest);
        Task<PromotionDto> DisablePromotion(int id);
        Task<IReadOnlyList<PromotionDto>> GetAllPromotions();
        Task<PromotionDto> GetPromotionById(int id);
    }   
}
