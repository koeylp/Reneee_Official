namespace Reneee.Domain.Entities
{
    public class ProductPromotion
    {
        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public int PromotionId { get; set; }
        public int Status { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
