using Example.Redis;
using Moq;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;
using Xunit;

namespace ExampleTests.Redis
{
    public interface IFoo
    {
        string TryGetStringValue();
        Task<string> TryGetStringValueAsync();
        T TryGetGenericValue<T>();
    }

    class AClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public char Initial { get; set; }
        public bool IsValid { get; set; }
    }

    public class RedisWrapperTest
    {
        private Mock<IDatabase> _mockDatabase;
        public RedisWrapperTest()
        {

        }

        [Fact]
        public void Get_Simple_Test()
        {
            var key = "123";
            var value = "Juan Manuel Fangio";
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);

            var result = wrapper.Get(key);
            Assert.Equal(value, result);
        }


        [Fact]
        public void Get_Sync_With_Direct_Reference_Test()
        {
            var key = "123";
            var value = "Juan Manuel Fangio";
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(value);


            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetStringValue())
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);

            var result = wrapper.Get(key, fooMock.Object.TryGetStringValue);
            Assert.Equal(value, result);
        }


        [Fact]
        public void Get_Sync_With_Local_Function_Test()
        {
            var key = "123";
            var value = "Juan Manuel Fangio";
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(value);


            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetStringValue())
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);

            string retrieveDataFromOtherDatabase()
            {
                return fooMock.Object.TryGetStringValue();
            }

            var result = wrapper.Get(key, retrieveDataFromOtherDatabase);
            Assert.Equal(value, result);
        }


        [Fact]
        public void Get_Sync_With_Inline_Function_Test()
        {
            var key = "123";
            var value = "Juan Manuel Fangio";
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(value);


            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetStringValue())
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);

            var result = wrapper.Get(key, () => fooMock.Object.TryGetStringValue());
            Assert.Equal(value, result);
        }


        [Fact]
        public void Get_Generic_Sync_With_Direct_Reference_Test()
        {
            var key = "123";
            var value = new AClass()
            {
                Id = 1,
                Name = "Pedro Picapiedras",
                Initial = 'F',
                IsValid = true,
                Price = 2.22
            };
            var valueStr = JsonConvert.SerializeObject(value);
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(valueStr);

            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetGenericValue<AClass>())
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);

            var result = wrapper.Get(key, fooMock.Object.TryGetGenericValue<AClass>);

            Assert.NotNull(result);
            Assert.Equal(value.Id, result.Id);
            Assert.Equal(value.Name, result.Name);
            Assert.Equal(value.Price, result.Price);
            Assert.Equal(value.Initial, result.Initial);
            Assert.Equal(value.IsValid, result.IsValid);
        }


        [Fact]
        public void Get_Generic_Sync_With_Local_Function_Test()
        {
            var key = "123";
            var value = new AClass()
            {
                Id = 1,
                Name = "Pedro Picapiedras",
                Initial = 'F',
                IsValid = true,
                Price = 2.22
            };
            var valueStr = JsonConvert.SerializeObject(value);
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(valueStr);

            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetGenericValue<AClass>())
                .Returns(value);


            var wrapper = new RedisWrapper(_mockDatabase.Object);

            AClass retrieveDataFromOtherDatabase()
            {
                return fooMock.Object.TryGetGenericValue<AClass>();
            }

            var result = wrapper.Get(key, retrieveDataFromOtherDatabase);
            Assert.NotNull(result);
            Assert.Equal(value.Id, result.Id);
            Assert.Equal(value.Name, result.Name);
            Assert.Equal(value.Price, result.Price);
            Assert.Equal(value.Initial, result.Initial);
            Assert.Equal(value.IsValid, result.IsValid);
        }


        [Fact]
        public void Get_Generic_Sync_With_Inline_Function_Test()
        {
            var key = "123";
            var value = new AClass()
            {
                Id = 1,
                Name = "Pedro Picapiedras",
                Initial = 'F',
                IsValid = true,
                Price = 2.22
            };
            var valueStr = JsonConvert.SerializeObject(value);
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(valueStr);

            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetGenericValue<AClass>())
                .Returns(value);


            var wrapper = new RedisWrapper(_mockDatabase.Object);

            var result = wrapper.Get(key, () => fooMock.Object.TryGetGenericValue<AClass>());
            Assert.NotNull(result);
            Assert.Equal(value.Id, result.Id);
            Assert.Equal(value.Name, result.Name);
            Assert.Equal(value.Price, result.Price);
            Assert.Equal(value.Initial, result.Initial);
            Assert.Equal(value.IsValid, result.IsValid);
        }

        [Fact]
        public async void Get_Async_With_Direct_Reference_Test()
        {
            var key = "123";
            var value = "Juan Manuel Fangio";
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(value);


            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetStringValue())
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);

            var result = await wrapper.Get(key, fooMock.Object.TryGetStringValueAsync);
            Assert.Equal(value, result);


        }

        [Fact]
        public async void Get_Async_With_Local_Function_Test()
        {
            var key = "123";
            var value = "Juan Manuel Fangio";
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(value);


            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetStringValue())
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);

            async Task<string> retrieveDataFromOtherDatabase()
            {
                return await fooMock.Object.TryGetStringValueAsync();
            }

            var result = await wrapper.Get(key, retrieveDataFromOtherDatabase);
            Assert.Equal(value, result);
        }

        [Fact]
        public async void Get_Async_With_Inline_Function_Test()
        {
            var key = "123";
            var value = "Juan Manuel Fangio";
            _mockDatabase = new Mock<IDatabase>();
            _mockDatabase
                .Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(value);


            Mock<IFoo> fooMock = new();
            fooMock.Setup(x => x.TryGetStringValue())
                .Returns(value);

            var wrapper = new RedisWrapper(_mockDatabase.Object);



            var result = await wrapper.Get(key, async () => await fooMock.Object.TryGetStringValueAsync());




            Assert.Equal(value, result);
        }
    }
}
