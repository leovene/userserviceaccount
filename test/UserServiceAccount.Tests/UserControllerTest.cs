using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Interfaces.Application;
using UserServiceAccount.Domain.ViewModels;
using Xunit;

namespace UserServiceAccount.Tests
{
    public class UserControllerTest
    {
        private readonly Mock<IBaseController<UserViewModel>> _mockedController;

        public UserControllerTest()
        {
            _mockedController = new Mock<IBaseController<UserViewModel>>();
        }

        [Fact]
        public async Task GetByQuantityTest()
        {
            //arrange
            _mockedController.Setup(x => x.GetAll()).ReturnsAsync(new List<UserViewModel>() { new Mock<UserViewModel>().Object });

            //act
            var getResult = await _mockedController.Object.GetAll();
            var result = getResult.ToArray();

            //assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            //arrange
            _mockedController.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(new Mock<UserViewModel>().Object);

            //act
            var result = await _mockedController.Object.Get(new Guid().ToString());

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<UserViewModel>(result);
        }

        [Fact]
        public async Task PostModelTest()
        {
            //arrange
            var mockedViewModel = new Mock<UserViewModel>();
            _mockedController.Setup(x => x.Post(It.IsAny<UserViewModel>())).ReturnsAsync(new Guid().ToString());

            //act
            var result = await _mockedController.Object.Post(mockedViewModel.Object);

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<string>(result);
        }

        [Fact]
        public async Task PutModelTest()
        {
            //arrange
            var mockedViewModel = new Mock<UserViewModel>();
            _mockedController.Setup(x => x.Put(It.IsAny<string>(), It.IsAny<UserViewModel>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedController.Object.Put(new Guid().ToString(), mockedViewModel.Object);

            //assert
            Assert.True(result > 0);
        }

        [Fact]
        public async Task DeleteModelTest()
        {
            //arrange
            var mockedViewModel = new Mock<UserViewModel>();
            _mockedController.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedController.Object.Delete(mockedViewModel.Object.Id);

            //assert
            Assert.True(result > 0);
        }
    }
}
