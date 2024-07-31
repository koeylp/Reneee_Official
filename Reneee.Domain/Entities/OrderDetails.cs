namespace Reneee.Domain.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductAttributeId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }

    }
}
