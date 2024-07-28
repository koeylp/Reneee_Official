namespace Reneee.Application.DTOs.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
    }
}
