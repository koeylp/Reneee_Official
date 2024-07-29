﻿using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    
    public class OrderRepository(ApplicationDbContext dbContext) : GenericRepository<Order>(dbContext), IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
