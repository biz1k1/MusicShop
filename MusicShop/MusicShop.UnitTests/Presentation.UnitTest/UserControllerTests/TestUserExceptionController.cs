using AutoMapper;
using Moq;
using MusicShop.Application.Common.Errors;
using MusicShop.Application.Services.ServiceHandler.User;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Repository;
using MusicShop.Presentation.Common.DTOs.User;
using MusicShop.Presentation.Controllers;

namespace MusicShop.Tests.Presentation.UnitTest.UserControllerTests
{
    public class TestUserExceptionController
    {
        UserController sut;
        Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        Mock<IRoleRepository> mockRoleRepository = new Mock<IRoleRepository>();
        Mock<IMapper> mockMapper = new Mock<IMapper>();
        Mock<IUserServiceHandler> mockUserServiceHandler = new Mock<IUserServiceHandler>();

        const int Id_Of_Non_Existent_User = 100;
        public TestUserExceptionController()
        {
            sut = new UserController(mockUnitOfWork.Object,mockMapper.Object,mockUserServiceHandler.Object);
        }
        [Fact]
        public async Task Success_when_GetUserById_method_catch_UserNotFound_exception_when_UserObject_NULL()
        {
            mockUnitOfWork.Setup(x => x.User).Returns(mockUserRepository.Object);

            var result = sut.GetUserById(Id_Of_Non_Existent_User);

            await Assert.ThrowsAsync<UserNotFound>(() => result);
        }
        [Fact]
        public async Task Success_when_RemoveUser_method_catch_UserNotFound_exception_when_UserObject_NULL()
        {
            mockUnitOfWork.Setup(x => x.User).Returns(mockUserRepository.Object);

            var result = sut.RemoveUser(Id_Of_Non_Existent_User);

            await Assert.ThrowsAsync<UserNotFound>(() => result);
        }
        [Fact]
        public async Task Success_when_UpdateUser_method_catch_UserNotFound_exception_when_UserObject_NULL()
        {
            mockUnitOfWork.Setup(x => x.User).Returns(mockUserRepository.Object);

            var result = sut.UpdateUser(new UserRequestUpdate());

            await Assert.ThrowsAsync<UserNotFound>(() => result);
        }
        [Fact]
        public async Task Success_when_UpdateUser_method_catch_UserNotFound_exception_when_UserRoleNULL()
        {
            mockUserRepository.Setup(x => x.GetUserIncludeRoleAsync(It.IsAny<int>())).ReturnsAsync(new UserEntity());
            mockUnitOfWork.Setup(x => x.Role).Returns(mockRoleRepository.Object);
            mockUnitOfWork.Setup(x => x.User).Returns(mockUserRepository.Object);

            var result = sut.UpdateUser(new UserRequestUpdate());

            await Assert.ThrowsAsync<InvalidRoleUser>(() => result);
        }
        [Fact]
        public async Task Success_when_UpdateUser_method_catch_UserNotFound_exception_when_RoleIdentityObject_NULL()
        {
            mockRoleRepository.Setup(x => x.GetUserWithExistRole(It.IsAny<string>())).ReturnsAsync(new RoleEntity());
            mockUserRepository.Setup(x => x.GetUserIncludeRoleAsync(It.IsAny<int>())).ReturnsAsync(new UserEntity());
            mockUnitOfWork.Setup(x => x.Role).Returns(mockRoleRepository.Object);
            mockUnitOfWork.Setup(x => x.User).Returns(mockUserRepository.Object);

            var result = sut.UpdateUser(new UserRequestUpdate());

            await Assert.ThrowsAsync<InvalidRoleUser>(() => result);
        }
        [Fact]
        public async Task Success_when_AddAdminUser_method_catch_UserNotFound_exception_when_RoleIdentity_NULL()
        {
            mockUnitOfWork.Setup(x => x.Role).Returns(mockRoleRepository.Object);

            var result = sut.AddAdminUser(new UserRequest());

            await Assert.ThrowsAsync<InvalidRoleUser>(() => result);
        }
    }
}
