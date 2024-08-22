using Reneee.Application.DTOs.ProductPromotion;
using Reneee.Application.DTOs.Promotion;

namespace Reneee.Application.Services
{
    public interface IPromotionService
    {
        Task<PromotionDto> CreatePromotion(CreatePromotionDto promotionRequest);
        Task<string> DeletePromotion(int id);
        Task<PromotionDto> DisablePromotion(int id);
        Task<IReadOnlyList<PromotionDto>> GetAllPromotions();
        Task<IReadOnlyList<ProductPromotionDto>> GetActiveProductPromotions();
        Task<PromotionDto> GetPromotionById(int id);
    }   
}
