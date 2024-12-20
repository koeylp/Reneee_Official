﻿using Microsoft.EntityFrameworkCore.Storage;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAttributeRepository AttributeRepository { get; }
        IAttributeValueRepository AttributeValueRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IProductAttributeRepository ProductAttributeRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductPromotionRepository ProductPromotionRepository { get; }
        IProductRepository ProductRepository { get; }
        IPromotionRepository PromotionRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }
        IExecutionStrategy CreateExecutionStrategy();
        ICommentRepository CommentRepository { get; }
        IResetPasswordRepository ResetPasswordRepository { get; }
        ISalesRepository SalesRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        IDisctrictRepository DisctrictRepository { get; }
        IWardRepository WardRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveChangesAsync();
    }
}
