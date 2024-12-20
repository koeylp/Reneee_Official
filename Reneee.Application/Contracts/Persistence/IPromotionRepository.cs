﻿using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IPromotionRepository : IGenericRepository<Promotion>
    {
        Task<IReadOnlyList<Promotion>> GetActivePromotions();
        Task<Promotion> GetByIdAndStatus(int id, int status);
    }
}
