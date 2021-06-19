using System.Collections.Generic;

using zoo_manager_backend.Services;
using zoo_manager_backend.Models;
using zoo_manager_backend.Exceptions;

using Moq;
using Xunit;
using MongoDB.Driver;
using zoo_manager_backend;

using zoo_manager_backend.Repositories;

namespace zoo_manager_tests
{
    public class AnimalSpecimensServiceTests
    {
        private readonly Mock<MongoRepository<AnimalSpecimen>> animalSpecimenRepositoryMock;
        private readonly Mock<MongoRepository<AnimalType>> animalTypeRepositoryMock;
        private readonly IAnimalSpecimenService _serivce;
        public AnimalSpecimensServiceTests()
        {
            animalSpecimenRepositoryMock = new Mock<MongoRepository<AnimalSpecimen>>(new MongoClient(Config.DB_CONNECTION_STRING));
            animalTypeRepositoryMock = new Mock<MongoRepository<AnimalType>>(new MongoClient(Config.DB_CONNECTION_STRING));
            _serivce = new AnimalSpecimenService(animalSpecimenRepositoryMock.Object, animalTypeRepositoryMock.Object);
        }

        [Fact]
        public void GetAll_GetValidData_ReturnListOfAnimalSpecimen()
        {
            animalSpecimenRepositoryMock.Setup(repo => repo.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Empty, null))
               .Returns(new List<AnimalSpecimen>() { new AnimalSpecimen(), new AnimalSpecimen() });

            var animalList = _serivce.GetAll();
            Assert.NotNull(animalList);

            var animalSpecimens = Assert.IsType<List<AnimalSpecimen>>(animalList);
            Assert.Equal(2, animalSpecimens.Count);
        }

        [Fact]
        public void Get_GetInvalidData_ReturnItemNotFoundException()
        {
            animalSpecimenRepositoryMock.Setup(repo => repo.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Empty, null))
               .Throws<ItemNotFoundException>();

            var isItemNotFoundException = Assert.Throws<ItemNotFoundException>(() => _serivce.Get(It.IsAny<int>()));
        }
        [Fact]
        public void Get_GetValidData_ReturnAnimalSpecimen()
        {
            int id = 1;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1 };

            animalSpecimenRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
                .Returns(new List<AnimalSpecimen> { new AnimalSpecimen { AnimalName = "Lion", Id = 1 } });

            var animalList = _serivce.Get(id);
            Assert.NotNull(animalList);

            var animalSpecimens = Assert.IsType<AnimalSpecimen>(animalList);
            Assert.Equal(animalSpecimen, animalSpecimens);

        }

        [Fact]
        public void Add_GetValidData_ReturnAnimalSpecimen()
        {
            int id = 1;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 3 };

            animalTypeRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalType>>(), null))
               .Returns(new List<AnimalType> { new AnimalType { Id = 3, TypeName = "TestName", TypeCategoryId = 2 } });

            animalSpecimenRepositoryMock.Setup(repo => repo.InsertOne(It.IsAny<AnimalSpecimen>()))
                .Returns(new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 3 });

            var animalList = _serivce.Add(animalSpecimen);
            Assert.NotNull(animalList);

            var animalSpecimens = Assert.IsType<AnimalSpecimen>(animalList);
            Assert.Equal(animalSpecimen, animalSpecimens);

        }
        [Fact]
        public void Add_GetInvalidData_ReturnRequiredItemNotExistsException()
        {
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 3 };

            animalTypeRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalType>>(), null))
               .Throws<RequiredItemNotExistsException>();

            Assert.Throws<RequiredItemNotExistsException>(() => _serivce.Add(animalSpecimen));
        }

        [Fact]
        public void Delete_GetValidData_ReturnAnimalSpecimen()
        {
            int id = 1;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = id, TypeId = 3 };

            animalSpecimenRepositoryMock.Setup(repo => repo.FindOneAndDelete(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
               .Returns(animalSpecimen);

            var animalList = _serivce.Delete(id);
            Assert.NotNull(animalList);

            var animalSpecimens = Assert.IsType<AnimalSpecimen>(animalList);
            Assert.Equal(animalSpecimen, animalSpecimens);

        }
        [Fact]
        public void Delete_GetInvalidData_ReturnItemNotFoundException()
        {
            int id = 1;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = id, TypeId = 3 };

            animalSpecimenRepositoryMock.Setup(repo => repo.FindOneAndDelete(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
               .Throws<ItemNotFoundException>();

            Assert.Throws<ItemNotFoundException>(() => _serivce.Delete(id));


        }
    }
}
