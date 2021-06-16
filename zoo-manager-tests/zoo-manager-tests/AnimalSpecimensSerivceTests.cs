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

    }
}
