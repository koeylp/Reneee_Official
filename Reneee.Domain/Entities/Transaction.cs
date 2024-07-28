﻿namespace Reneee.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Status { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }

    }
}
