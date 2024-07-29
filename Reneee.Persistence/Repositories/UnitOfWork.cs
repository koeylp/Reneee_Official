using Reneee.Application.Contracts;

namespace Reneee.Persistence.Repositories
{
    public class UnitOfWork(ApplicationDbContext context,
                      IAttributeRepository attributeRepository,
                      IAttributeValueRepository attributeValueRepository,
                      ICategoryRepository categoryRepository,
                      IOrderDetailsRepository orderDetailsRepository,
                      IOrderRepository orderRepository,
                      IPaymentRepository paymentRepository,
                      IProductAttributeRepository productAttributeRepository,
                      IProductImageRepository productImageRepository,
                      IProductPromotionRepository productPromotionRepository,
                      IProductRepository productRepository,
                      IPromotionRepository promotionRepository,
                      ITransactionRepository transactionRepository,
                      IUserRepository userRepository) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        public IAttributeRepository Attributes { get; } = attributeRepository;
        public IAttributeValueRepository AttributeValues { get; } = attributeValueRepository;
        public ICategoryRepository Categories { get; } = categoryRepository;
        public IOrderDetailsRepository OrderDetails { get; } = orderDetailsRepository;
        public IOrderRepository Orders { get; } = orderRepository;
        public IPaymentRepository Payments { get; } = paymentRepository;
        public IProductAttributeRepository ProductAttributes { get; } = productAttributeRepository;
        public IProductImageRepository ProductImages { get; } = productImageRepository;
        public IProductPromotionRepository ProductPromotions { get; } = productPromotionRepository;
        public IProductRepository Products { get; } = productRepository;
        public IPromotionRepository Promotions { get; } = promotionRepository;
        public ITransactionRepository Transactions { get; } = transactionRepository;
        public IUserRepository Users { get; } = userRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
