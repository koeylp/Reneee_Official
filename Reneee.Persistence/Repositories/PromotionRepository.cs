﻿using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class PromotionRepository(ApplicationDbContext dbContext) : GenericRepository<Promotion>(dbContext), IPromotionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
