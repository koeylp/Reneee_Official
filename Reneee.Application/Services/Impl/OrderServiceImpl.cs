using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Application.DTOs.Order;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

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
                                  ISalesRepository salesRepository,
                                  IMailService mailService,
                                  IServiceScopeFactory serviceScopeFactory,
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
        private readonly ISalesRepository _salesRepository = salesRepository;
        private readonly IMailService _mailService = mailService;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
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
                        var productAttributeEntity = await _productAttributeRepository.Get(item.ProductAttribute.Id)
                        ?? throw new BadRequestException("Product attribute not found with id " + item.ProductAttribute.Id);
                        productAttributeEntity.Stock += item.Quantity;
                        if (productAttributeEntity.Status == 0)
                        {
                            productAttributeEntity.Status = 1;
                        }
                        await _productAttributeRepository.Update(productAttributeEntity);
                        await _salesRepository.DeleteSalesByProductAttribute(productAttributeEntity);

                        var productEntity = await _productRepository.Get(item.ProductAttribute.ProductID);
                        productEntity.TotalQuantity += item.Quantity;
                        productEntity.unitSold -= item.Quantity;
                        if (productEntity.Status == 0)
                        {
                            productEntity.Status = 1;
                        }
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
                var userEntity = await _userService.GetUserFromEmailClaims();
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

                        var sales = new Sales
                        {
                            ProductAttribute = productAttributeEntity,
                            SalesDate = DateTime.Now,
                            Status = 1,
                            TotalSales = item.Price * item.Quantity,
                            UnitsSold = item.Quantity
                        };
                        await _salesRepository.Add(sales);
                        if (productAttributeEntity.Stock - item.Quantity < 0)
                            throw new BadRequestException($"{productAttributeEntity.Product.Name} got out of stock");
                        if (productAttributeEntity.Stock < item.Quantity)
                        {
                            throw new BadRequestException($"{productAttributeEntity.Product.Name} got out of stock");
                        }
                        if (productAttributeEntity.Stock <= 0)
                        {
                            throw new BadRequestException($"{productAttributeEntity.Product.Name} got out of stock");
                        }

                        

                        productAttributeEntity.Stock -= item.Quantity;
                        if (productAttributeEntity.Stock == 0)
                        {
                            productAttributeEntity.Status = 0;
                        }
                        await _productAttributeRepository.Update(productAttributeEntity);

                        var productEntity = await _productRepository.Get(productAttributeEntity.ProductID);
                        productEntity.TotalQuantity -= item.Quantity;
                        if (productEntity.TotalQuantity - item.Quantity < 0)
                            throw new BadRequestException($"{productEntity.Name} got out of stock");
                        if (productEntity.TotalQuantity == 0)
                        {
                            productEntity.Status = 0;
                        }
                        productEntity.unitSold += item.Quantity;
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

                    var orderDto = _mapper.Map<OrderDto>(savedOrder);
                    var name = userEntity.FirstName + " " + userEntity.LastName;

                    //await _mailService.SendOrderConfirmationEmail(userEntity.Email, orderDto, name);

                    _ = Task.Run(async () => await CheckOrderStatusAsync(savedOrder.Id));
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();


                    return orderDto;
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
            var userEntity = await _userService.GetUserFromEmailClaims();
            var orderEntities = await _orderRepository.GetOrderByUserAndStatus(userEntity, status);
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

        private async Task CheckOrderStatusAsync(int orderId)
        {
            await Task.Delay(TimeSpan.FromMinutes(3));

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var productAttributeRepository = scope.ServiceProvider.GetRequiredService<IProductAttributeRepository>();
                var salesRepository = scope.ServiceProvider.GetRequiredService<ISalesRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var order = await orderRepository.Get(orderId);

                if (order != null && order.Status == 0)
                {
                    order.Status = -1;
                    order.UpdatedAt = DateTime.Now;

                    foreach (var item in order.OrderDetails)
                    {
                        var productAttributeEntity = await productAttributeRepository.Get(item.ProductAttribute.Id)
                        ?? throw new BadRequestException("Product attribute not found with id " + item.ProductAttribute.Id);
                        productAttributeEntity.Stock += item.Quantity;
                        if (productAttributeEntity.Status == 0)
                        {
                            productAttributeEntity.Status = 1;
                        }
                        await productAttributeRepository.Update(productAttributeEntity);
                        await salesRepository.DeleteSalesByProductAttribute(productAttributeEntity);

                        var productEntity = await productRepository.Get(item.ProductAttribute.ProductID);
                        productEntity.TotalQuantity += item.Quantity;
                        productEntity.unitSold -= item.Quantity;
                        if (productEntity.Status == 0)
                        {
                            productEntity.Status = 1;
                        }
                        await productRepository.Update(productEntity);
                    }

                    await orderRepository.Update(order);
                    await unitOfWork.SaveChangesAsync();
                    _logger.LogInformation($"Order {orderId} was canceled due to non-payment after 24 hours.");
                }
            }
        }


    }
}
