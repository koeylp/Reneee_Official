namespace Reneee.Domain.Entities
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int AttributeValueID { get; set; }
        public decimal AttributePrice { get; set; }
        public decimal AttributeDiscountPrice { get; set; }
        public int Stock { get; set; }
        public int Status { get; set; }
        public virtual Product Product { get; set; }
        public virtual AttributeValue AttributeValue { get; set; }
    }
}
