using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Application.DTOs.Order;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;
using System.Security.Claims;

namespace Reneee.Application.Services.Impl
{
    public class OrderServiceImpl(IOrderRepository orderRepository,
                                  IOrderDetailsRepository orderDetailsRepository,
                                  IPaymentRepository paymentRepository,
                                  IProductAttributeRepository productAttributeRepository,
                                  IUserRepository userRepository,
                                  IProductRepository productRepository,
                                  IUnitOfWork unitOfWork,
                                  ILogger<OrderServiceImpl> logger,
                                  IUserService userService,
                                  ICacheService cacheService,
                                  IMapper mapper) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository = orderDetailsRepository;
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IProductAttributeRepository _productAttributeRepository = productAttributeRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IUserService _userService = userService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<OrderServiceImpl> _logger = logger;
        private readonly ICacheService _cacheService = cacheService;
        private readonly IMapper _mapper = mapper;

        public async Task<OrderDto> CancelOrder(int id)
        {
            _logger.LogInformation("Entering method cancel order");

            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var orderEntity = await GetOrder(id);
                    orderEntity.Status = -1;
                    orderEntity.UpdatedAt = DateTime.Now;

                    var orderDetails = await _orderDetailsRepository.GetOrderDetailsByOrderId(id);
                    foreach (var item in orderDetails)
                    {
                        var productAttributeEntity = await _productAttributeRepository.Get(item.ProductAttribute.Id);
                        productAttributeEntity.Stock += item.Quantity;
                        await _productAttributeRepository.Update(productAttributeEntity);

                        var productEntity = await _productRepository.Get(item.ProductAttribute.ProductID);
                        productEntity.TotalQuantity += item.Quantity;
                        await _productRepository.Update(productEntity);
                    }

                    await _orderRepository.Update(orderEntity);
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<OrderDto>(orderEntity);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while cancelling order");
                    await transaction.RollbackAsync();
                    throw;
                }

            });

        }

        public async Task<OrderDto> CreateOrder(CreateOrderDto orderRequest)
        {
            _logger.LogInformation("Entering method CreateOrder with body CreateOrderDto");

            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                //var userEntity = await _userService.GetUserFromEmailClaims();
                var userEntity = await _userRepository.Get(1);
                var lockKey = $"lock_order_{Guid.NewGuid()}_{userEntity.Id}";

                var lockAcquired = await _cacheService.AcquireLockAsync(lockKey, TimeSpan.FromMinutes(1));

                if (!lockAcquired)
                {
                    _logger.LogWarning("Failed to acquire lock for order creation.");
                    throw new Exception("Could not acquire lock. Please try again later.");
                }

                try
                {

                    var foundPayment = await _paymentRepository.Get(orderRequest.PaymentId)
                                            ?? throw new NotFoundException($"Payment with id {orderRequest.PaymentId} not found");

                    var orderEntity = new Order
                    {
                        Address = orderRequest.Address,
                        OrderDate = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Total = orderRequest.Total,
                        User = userEntity,
                        Payment = foundPayment,
                        Status = 0,
                    };

                    var savedOrder = await _orderRepository.Add(orderEntity);
                    var orderDetailsEntities = new List<OrderDetails>();

                    foreach (var item in orderRequest.createOrderDetails)
                    {
                        var productAttributeEntity = await _productAttributeRepository.Get(item.ProductAttributeId)
                                                    ?? throw new NotFoundException($"Product Attribute with id {item.ProductAttributeId} not found");

                        if (productAttributeEntity.Stock <= 0)
                        {
                            throw new BadRequestException($"{productAttributeEntity.Product.Name} got out of stock");
                        }

                        productAttributeEntity.Stock -= item.Quantity;
                        if (productAttributeEntity.Stock == 0)
                        {
                            productAttributeEntity.Status = -1;
                        }
                        await _productAttributeRepository.Update(productAttributeEntity);

                        var productEntity = await _productRepository.Get(productAttributeEntity.ProductID);
                        productEntity.TotalQuantity -= item.Quantity;
                        await _productRepository.Update(productEntity);

                        var orderDetailsEntity = new OrderDetails
                        {
                            Order = savedOrder,
                            Price = item.Price,
                            ProductAttribute = productAttributeEntity,
                            Quantity = item.Quantity,
                            Status = 1
                        };
                        orderDetailsEntities.Add(orderDetailsEntity);
                    }

                    await _orderDetailsRepository.AddRange(orderDetailsEntities);
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return _mapper.Map<OrderDto>(savedOrder);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating order");
                    await transaction.RollbackAsync();
                    throw;
                }
                finally
                {
                    await _cacheService.ReleaseLockAsync(lockKey);
                }
            });
        }


        public async Task<IReadOnlyList<OrderDto>> GetAllOrders()
        {
            IReadOnlyList<Order> orderEntities = await _orderRepository.GetAll();
            var orderDtos = _mapper.Map<List<OrderDto>>(orderEntities);
            var sortedOrders = orderDtos.OrderByDescending(order => order.UpdatedAt).ToList();
            return sortedOrders.AsReadOnly();
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var orderEntity = await GetOrder(id);
            return _mapper.Map<OrderDto>(orderEntity);
        }

        public async Task<IReadOnlyList<OrderDto>> GetOrdersByUser(int status)
        {
            //var userEntity = await _userService.GetUserFromEmailClaims();
            var user = await _userRepository.Get(1)
                            ?? throw new NotFoundException("User not found");
            var orderEntities = await _orderRepository.GetOrderByUserAndStatus(user, status);
            var orderDtos = _mapper.Map<List<OrderDto>>(orderEntities);
            var sortedOrders = orderDtos.OrderByDescending(order => order.OrderDate).ToList();
            return sortedOrders.AsReadOnly();
        }

        public async Task<OrderDto> UpdateOrderStatus(int id, int status)
        {
            var orderEntity = await GetOrder(id);
            orderEntity.Status = status;
            orderEntity.UpdatedAt = DateTime.Now;
            await _orderRepository.Update(orderEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderDto>(orderEntity);
        }

        private async Task<Order> GetOrder(int id)
        {
            var orderEntity = await _orderRepository.Get(id)
                            ?? throw new NotFoundException($"Order not found with id {id}");
            return orderEntity;
        }
    }
}
