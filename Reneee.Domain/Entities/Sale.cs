namespace Reneee.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public int TotalSales { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
    }
}
