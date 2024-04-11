using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;

namespace MusicShop.Application.Services.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly IUnitOfWork _unitOfWork;
        public DbInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<UserEntity>> SeedUserAdmin()
        {
            List<UserEntity> userEntities = new List<UserEntity>();
            var admin = new UserEntity
            {
                Email = "admin@mail.ru",
                Login = "admin",
                Password = "admin",
                Roles = [await _unitOfWork.Role.GetByIdAsync((int)Role.Admin)]

            };
            var user = new UserEntity
            {
                Email = "user@mail.ru",
                Login = "user",
                Password = "user",
                Roles = [await _unitOfWork.Role.GetByIdAsync((int)Role.User)]

            };
            userEntities.Add(user);
            userEntities.Add(admin);
            return userEntities;

        }
    }
}

