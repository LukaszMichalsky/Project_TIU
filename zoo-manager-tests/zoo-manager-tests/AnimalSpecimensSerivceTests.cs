using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Controllers;
using zoo_manager_backend.Services;
using zoo_manager_backend.Models;
using zoo_manager_backend.Exceptions;

using Moq;
using Xunit;
using MongoDB.Driver;
using zoo_manager_backend;
using Xunit.Abstractions;
using Xunit.Sdk;
using zoo_manager_backend.Repositories;

namespace zoo_manager_tests
{
    public class AnimalSpecimensSerivceTests
    {
        private readonly Mock<AnimalSpecimenService> animalSpecimenMock;
        private readonly Mock<AnimalTypeService> animalTypeMock;
        private readonly Mock<MongoRepository<AnimalSpecimen>> animalSpecimenRepositoryMock;
        private readonly Mock<MongoRepository<AnimalType>> animalTypeRepositoryMock;
        private readonly AnimalSpecimenController _controller;
        public AnimalSpecimensSerivceTests()
        {
            animalSpecimenRepositoryMock = new Mock<MongoRepository<AnimalSpecimen>>(new MongoClient(Config.DB_CONNECTION_STRING));
            animalTypeRepositoryMock = new Mock<MongoRepository<AnimalType>>(new MongoClient(Config.DB_CONNECTION_STRING));
            animalSpecimenMock = new Mock<AnimalSpecimenService>(animalSpecimenRepositoryMock.Object, animalTypeRepositoryMock.Object);
            animalTypeMock = new Mock<AnimalTypeService>();
            _controller = new AnimalSpecimenController(animalSpecimenMock.Object);
        }


        [Fact]
        public void GetAllAnimalSpecimens_CallAction_GetListOfAnimalSpecimens()
        {
            animalSpecimenRepositoryMock.Setup(repo => repo.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Empty, null))
               .Returns(new List<AnimalSpecimen>() { new AnimalSpecimen(), new AnimalSpecimen() });

            // act
            var okResult = _controller.GetAllAnimalSpecimens();
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);

            var model = okObjectResult.Value as List<AnimalSpecimen>;
            Assert.NotNull(model);

            var animalSpecimens = Assert.IsType<List<AnimalSpecimen>>(okObjectResult.Value);
            Assert.Equal(2, animalSpecimens.Count);

        }

        [Fact]
        public void GetAllAnimalSpecimensWithId_CallActionWithValid_GetAnimalSpecimensById()
        {
            int id = 1;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1 };

            animalSpecimenRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
                .Returns(new List<AnimalSpecimen> { new AnimalSpecimen { AnimalName = "Lion", Id = 1 } });

            var okResult = _controller.GetSingleAnimalSpecimen(id);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);



            var animalSpecimensResult = Assert.IsType<AnimalSpecimen>(okObjectResult.Value);
            Assert.Equal(animalSpecimen, animalSpecimensResult);

        }

        [Fact]
        public void GetAllAnimalSpecimensWithId_CallActionWithInValid_ThrowsItemNotFoundException()
        {
            int id = 1;

            animalSpecimenRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
                .Returns(new List<AnimalSpecimen> { });

            var isItemNotFoundException = Assert.Throws<ItemNotFoundException>(() => animalSpecimenMock.Object.Get(id));

            var okResult = _controller.GetSingleAnimalSpecimen(id);
            Assert.NotNull(okResult);
            var NotFoundObjectResult = Assert.IsType<NotFoundResult>(okResult);


        }

        [Fact]
        public void AddAnimalSpecimen_CallWithValidArgs_ReturnedAddedAnimalSpecimen()
        {
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 2 };

            animalTypeRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalType>>(), null))
                .Returns(new List<AnimalType> { new AnimalType { Id = 3, TypeName = "TestName", TypeCategoryId = 2 } });

            animalSpecimenRepositoryMock.Setup(repo => repo.InsertOne(It.IsAny<AnimalSpecimen>()))
               .Returns(new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 2 });

            var okResult = _controller.AddAnimalSpecimen(animalSpecimen);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);

            var model = okObjectResult.Value as AnimalSpecimen;
            Assert.NotNull(model);

            var animalSpecimensResult = Assert.IsType<AnimalSpecimen>(okObjectResult.Value);
            Assert.Equal(animalSpecimen, animalSpecimensResult);

        }
        [Fact]
        public void AddAnimalSpecimen_CallWithInValidTyprArg_ReturnedRequiredItemNotExistsException()
        {
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 2 };

            animalTypeRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalType>>(), null))
                .Returns(new List<AnimalType> { });

            animalSpecimenRepositoryMock.Setup(repo => repo.InsertOne(It.IsAny<AnimalSpecimen>()))
               .Returns(new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 2 });

            Assert.Throws<RequiredItemNotExistsException>(() => animalSpecimenMock.Object.Add(animalSpecimen));

            var okResult = _controller.AddAnimalSpecimen(animalSpecimen);
            Assert.NotNull(okResult);
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(okResult);

        }
        [Fact]
        public void AddAnimalSpecimen_CallWithExistingAnimal_ReturnedBadRequest() //TODO -> jak juz taki jest to zwraca {} i ok status
        {
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { };

            animalTypeRepositoryMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalType>>(), null))
                .Returns(new List<AnimalType> { new AnimalType { Id = 2, TypeCategoryId = 3, TypeName = "TestName" } });

            animalSpecimenRepositoryMock.Setup(repo => repo.InsertOne(It.IsAny<AnimalSpecimen>()))
              .Returns(new AnimalSpecimen { });

            var okResult = _controller.AddAnimalSpecimen(animalSpecimen);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);

            var animalSpecimensResult = Assert.IsType<AnimalSpecimen>(okObjectResult.Value);
            Assert.Equal(animalSpecimen, animalSpecimensResult);
        }


        [Fact]
        public void DeleteAnimalSpecimen_CallForNonExistingAnimal_ReturnBadRequest() //TODO
        {
            int id = 1;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 2 };

            animalSpecimenRepositoryMock.Setup(repo => repo.FindOneAndDelete(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
                .Returns((AnimalSpecimen) null);

            var isItemNotFoundException = Assert.Throws<ItemNotFoundException>(() => animalSpecimenMock.Object.Delete(id));

            var okResult = _controller.DeleteAnimalSpecimen(id);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<NotFoundResult>(okResult);
        }

    }
}
