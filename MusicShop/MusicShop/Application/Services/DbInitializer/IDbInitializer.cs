using MusicShop.Domain.Model.Core;

namespace MusicShop.Application.Services.DbInitializer
{
    public interface IDbInitializer
    {
        Task<List<UserEntity>> SeedUserAdmin();
    }
}
