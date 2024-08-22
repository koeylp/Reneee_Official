using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Reneee.Application.Contracts.Persistence;

namespace Reneee.Persistence.Repositories
{
    public class UnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private IAttributeRepository? _attributeRepository;
        private IAttributeValueRepository? _attributeValueRepository;
        private ICategoryRepository? _categoryRepository;
        private IOrderDetailsRepository? _orderDetailsRepository;
        private IOrderRepository? _orderRepository;
        private IPaymentRepository? _paymentRepository;
        private IProductAttributeRepository? _productAttributeRepository;
        private IProductImageRepository? _productImageRepository;
        private IProductPromotionRepository? _productPromotionRepository;
        private IProductRepository? _productRepository;
        private IPromotionRepository? _promotionRepository;
        private ITransactionRepository? _transactionRepository;
        private IUserRepository? _userRepository;
        private IDbContextTransaction _currentTransaction;
        private ICommentRepository? _commentRepository;
        private IResetPasswordRepository? _resetPasswordRepository;
        private ISalesRepository? _salesRepository;


        public IAttributeRepository AttributeRepository =>
            _attributeRepository ??= new AttributeRepository(_context);
        public IAttributeValueRepository AttributeValueRepository =>
            _attributeValueRepository ??= new AttributeValueRepository(_context);
        public ICategoryRepository CategoryRepository =>
            _categoryRepository ??= new CategoryRepository(_context);
        public IOrderDetailsRepository OrderDetailsRepository =>
            _orderDetailsRepository ??= new OrderDetailsRepository(_context);
        public IOrderRepository OrderRepository =>
            _orderRepository ??= new OrderRepository(_context);
        public IPaymentRepository PaymentRepository =>
            _paymentRepository ??= new PaymentRepository(_context);
        public IProductAttributeRepository ProductAttributeRepository =>
            _productAttributeRepository ??= new ProductAttributeRepository(_context);
        public IProductImageRepository ProductImageRepository =>
            _productImageRepository ??= new ProductImageRepository(_context);
        public IProductPromotionRepository ProductPromotionRepository =>
            _productPromotionRepository ??= new ProductPromotionRepository(_context);
        public IProductRepository ProductRepository =>
            _productRepository ??= new ProductRepository(_context);
        public IPromotionRepository PromotionRepository =>
            _promotionRepository ??= new PromotionRepository(_context);
        public ITransactionRepository TransactionRepository =>
            _transactionRepository ??= new TransactionRepository(_context);
        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context);
        public ICommentRepository CommentRepository =>
            _commentRepository ??= new CommentRepository(_context);
        public IResetPasswordRepository ResetPasswordRepository =>
            _resetPasswordRepository ??= new ResetPasswordRepository(_context);
        public ISalesRepository SalesRepository =>
            _salesRepository ??= new SalesRepository(_context);

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _context.Database.CreateExecutionStrategy();
        }
    }
}
