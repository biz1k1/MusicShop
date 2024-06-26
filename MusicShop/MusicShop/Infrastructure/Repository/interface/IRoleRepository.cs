﻿using MusicShop.Domain.Model.Aunth;

namespace MusicShop.Infrastructure.Repository
{
    public interface IRoleRepository : IRepository<RoleEntity>
    {
        Task<RoleEntity?> GetUserWithExistRole(string? role);
    }
}