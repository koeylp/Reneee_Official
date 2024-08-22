namespace Reneee.Domain.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public decimal TotalSales { get; set; }
        public DateTime SalesDate { get; set; }
        public int UnitsSold { get; set; }
        public int Status { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
