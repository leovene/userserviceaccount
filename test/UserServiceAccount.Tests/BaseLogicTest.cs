using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Business;
using Xunit;

namespace UserServiceAccount.Tests
{
    public sealed class BaseLogicTest
    {
        private readonly Mock<IBaseLogic<BaseEntity>> _mockedLogic;

        public BaseLogicTest()
        {
            _mockedLogic = new Mock<IBaseLogic<BaseEntity>>();
        }

        [Fact]
        public async Task GetByQuantityTest()
        {
            //arrange
            _mockedLogic.Setup(x => x.GetAll()).ReturnsAsync(new List<BaseEntity>() { new Mock<BaseEntity>().Object });

            //act
            var getResult = await _mockedLogic.Object.GetAll();
            var result = getResult.ToArray();

            //assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            //arrange
            _mockedLogic.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(new Mock<BaseEntity>().Object);

            //act
            var result = await _mockedLogic.Object.Get(new Guid());

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BaseEntity>(result);
        }

        [Fact]
        public async Task PostModelTest()
        {
            //arrange
            var mockedModel = new Mock<BaseEntity>();
            _mockedLogic.Setup(x => x.Post(It.IsAny<BaseEntity>())).ReturnsAsync(new Guid());

            //act
            var result = await _mockedLogic.Object.Post(mockedModel.Object);

            //assert
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task PutModelTest()
        {
            //arrange
            var mockedModel = new Mock<BaseEntity>();
            _mockedLogic.Setup(x => x.Put(It.IsAny<BaseEntity>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedLogic.Object.Put(mockedModel.Object);

            //assert
            Assert.True(result > 0);
        }

        [Fact]
        public async Task DeleteModelTest()
        {
            var mockedModel = new Mock<BaseEntity>();
            _mockedLogic.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedLogic.Object.Delete(mockedModel.Object.Id);

            //assert
            Assert.True(result > 0);
        }
    }
}
