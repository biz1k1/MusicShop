using Moq;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.UnitTests.Infrastructure.Repository.Mock.MockRepository
{
    public class MockUserRepository
    {
        public static Mock<IUserRepository> GetUserRepository()
        {
            var users= new List<UserEntity>
            {
                new UserEntity
                {
                    Id=1,
                    Login="User 1",
                },
                new UserEntity
                {
                    Id=2,
                    Login="User 2",
                },
                new UserEntity
                {
                    Id=3,
                    Login="User 3",
                },


            };
            var userById = users.FirstOrDefault(x => x.Id == 1);
            var userByLogin = users.FirstOrDefault(x => x.Login == "User 1");
            // generic repository methods
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
            mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(userById);
            mockRepository.Setup(x => x.Remove(It.IsAny<UserEntity>())).Callback(() =>
            {
                users.Remove(userById);
                return;
            });
            mockRepository.Setup(x => x.Add(It.IsAny<UserEntity>())).Callback((UserEntity product) =>
            {
                users.Add(product);
                return;
            });
            mockRepository.Setup(x => x.Update(It.IsAny<UserEntity>())).Callback((UserEntity category) =>
            {
                userById.Id = 4;
                userById.Login = "User 4";
            });
            // unique method for repository
            mockRepository.Setup(x => x.GetAllUsersIncludeRoleAsync()).ReturnsAsync(users);
            mockRepository.Setup(x => x.GetUserIncludeRoleAsync(It.IsAny<int>())).ReturnsAsync(userById);
            mockRepository.Setup(x => x.GetUserByLoginAsync(It.IsAny<string>())).ReturnsAsync(userByLogin);
            return mockRepository;
        }
    }
}
