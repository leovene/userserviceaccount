using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Infra;
using Xunit;

namespace UserServiceAccount.Tests
{
    public sealed class BaseRepositoryTest
    {
        private readonly Mock<IBaseRepository<BaseEntity>> _mockedRepository;

        public BaseRepositoryTest()
        {
            _mockedRepository = new Mock<IBaseRepository<BaseEntity>>();
        }

        [Fact]
        public async Task SelectAllTest()
        {
            //arrange
            var itemMock = new Mock<BaseEntity>();
            var items = new List<BaseEntity> { itemMock.Object };
            _mockedRepository.Setup(x => x.SelectAll()).ReturnsAsync(items);

            //act
            var result = await _mockedRepository.Object.SelectAll();

            //assert
            Assert.Single(result);
        }

        [Fact]
        public async Task SelectByIdTest()
        {
            //arrange
            _mockedRepository.Setup(x => x.Select(It.IsAny<Guid>())).ReturnsAsync(new Mock<BaseEntity>().Object);

            //act
            var result = await _mockedRepository.Object.Select(new Guid());

            //assert
            Assert.NotNull(result);
        }


        [Fact]
        public void InsertOneEntityAndGetByIdTest()
        {
            //arrange
            var mockedEntity = new Mock<BaseEntity>();
            _mockedRepository.Setup(x => x.Insert(It.IsAny<BaseEntity>())).Returns(new Guid());

            //act
            var guid = _mockedRepository.Object.Insert(mockedEntity.Object);

            //assert
            Assert.IsType<Guid>(guid);
        }

        [Fact]
        public void UpdateOneEntityAndGetByIdTest()
        {
            //arrange
            var mockedEntity = new Mock<BaseEntity>();
            _mockedRepository.Setup(x => x.Update(It.IsAny<BaseEntity>())).Verifiable();

            //act
            _mockedRepository.Object.Update(mockedEntity.Object);

            //assert
            _mockedRepository.Verify(x => x.Update(It.IsAny<BaseEntity>()), Times.Once);
        }

        [Fact]
        public void DeleteEntityTest()
        {
            //arrange
            var mockedEntity = new Mock<BaseEntity>();
            _mockedRepository.Setup(x => x.Delete(It.IsAny<Guid>())).Verifiable();

            //act
            _mockedRepository.Object.Delete(mockedEntity.Object.Id);

            //assert
            _mockedRepository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task SaveAsyncTest()
        {
            //arrange
            _mockedRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(new Random().Next);

            //act
            var result = await _mockedRepository.Object.SaveChangesAsync().ConfigureAwait(false);

            //assert
            Assert.IsType<int>(result);
        }
    }
}
