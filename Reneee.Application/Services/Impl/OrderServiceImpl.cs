﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
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
                                  IUnitOfWork unitOfWork,
                                  ILogger<OrderServiceImpl> logger,
                                  IMapper mapper) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository = orderDetailsRepository;
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IProductAttributeRepository _productAttributeRepository = productAttributeRepository;
        private readonly IUserRepository _userRepository = userRepository;  
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<OrderServiceImpl> _logger = logger;
        private readonly IMapper _mapper = mapper;
        public async Task<OrderDto> CreateOrder(CreateOrderDto orderRequest)
        {
            _logger.LogInformation("Entering method CreateOeder with body CreateOrderDto");

            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var userEntity = await _userRepository.Get(1); 
                    var foundPayment = await _paymentRepository.Get(orderRequest.PaymentId)
                                            ?? throw new NotFoundException($"Payment with id {orderRequest.PaymentId} not found");
                    var orderEntity = new Order
                    {
                        Address = orderRequest.Address,
                        OrderDate = DateTime.Now,
                        Total = orderRequest.Total,
                        User = userEntity,
                        Payment = foundPayment,
                        Status = 0,
                    };
                    var savedOrder = await _orderRepository.Add(orderEntity);
                    var orderDetailsEntities = new List<OrderDetails>();
                    foreach (var item in orderRequest.createOrderDetails)
                    {
                        var productAttributeEntity = await _productAttributeRepository.Get(item.ProductAttribtueId)
                                                    ?? throw new NotFoundException($"Product Attribute with id {item.ProductAttribtueId} not found");
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
            });
        }
    }
}
