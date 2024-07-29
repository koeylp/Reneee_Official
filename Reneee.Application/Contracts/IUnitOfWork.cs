namespace Reneee.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAttributeRepository Attributes { get; }
        IAttributeValueRepository AttributeValues { get; }
        ICategoryRepository Categories { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        IProductAttributeRepository ProductAttributes { get; }
        IProductImageRepository ProductImages { get; }
        IProductPromotionRepository ProductPromotions { get; }
        IProductRepository Products { get; }
        IPromotionRepository Promotions { get; }
        ITransactionRepository Transactions { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
