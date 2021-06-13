using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Controllers;
using zoo_manager_backend.Services;
using zoo_manager_backend.Models;

using Moq;
using Xunit;
using MongoDB.Driver;
using zoo_manager_backend;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace zoo_manager_tests
{
    public class EmployeesControllerTests
    {
        //private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly Mock<MongoService<AnimalSpecimen>> animalSpecimenMock;
        private readonly Mock<MongoService<AnimalType>> animalTypeMock;
        private readonly AnimalSpecimensController _controller;
        public EmployeesControllerTests()
        {
            animalSpecimenMock = new Mock<MongoService<AnimalSpecimen>>(new MongoClient(Config.DB_CONNECTION_STRING)); //todo: mock mongo
            animalTypeMock = new Mock<MongoService<AnimalType>>(new MongoClient(Config.DB_CONNECTION_STRING)); //todo: mock mongo
            _controller = new AnimalSpecimensController(animalSpecimenMock.Object, animalTypeMock.Object);
        }

        [Fact]
        public void GetAllAnimalSpecimens_CallAction_GetListOfAnimalSpecimens()
        {
            // act
            var okResult = _controller.GetAllAnimalSpecimens();
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);

            //Assert.IsType<List<AnimalSpecimen>>(okResult.ToString());
            //var result = okResult as ViewResult; //cast okbjectView into viewResult



        }

        [Fact]
        public void GetAllAnimalSpecimens_CallAction_GetAcutalNumberOfAnimalSpecimens()
        {
            animalSpecimenMock.Setup(repo => repo.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Empty, null))
                .Returns(new List<AnimalSpecimen>() { new AnimalSpecimen(), new AnimalSpecimen() });
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

            animalSpecimenMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
                .Returns(new List<AnimalSpecimen> { new AnimalSpecimen { AnimalName = "Lion", Id = 1 } });

            var okResult = _controller.GetSingleAnimalSpecimen(id);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);



            var animalSpecimensResult = Assert.IsType<AnimalSpecimen>(okObjectResult.Value);
            Assert.Equal(animalSpecimen, animalSpecimensResult);

        }
        [Fact]
        public void GetAllAnimalSpecimensWithId_CallActionWithInvalid_GetAnimalSpecimensById()
        {
            int id = 2;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1 };

            animalSpecimenMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
                .Returns(new List<AnimalSpecimen> { });

            var okResult = _controller.GetSingleAnimalSpecimen(id);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<NotFoundObjectResult>(okResult);

            var animalSpecimensResult = Assert.IsType<object>(okObjectResult.Value);
        }
        [Fact]
        public void GetAllAnimalSpecimensWithDuplicatedId_CallActionWithId_ReturnNotFoundObject()
        {
            int id = 1;
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1 };

            animalSpecimenMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalSpecimen>>(), null))
                .Returns(new List<AnimalSpecimen> { new AnimalSpecimen { AnimalName = "Lion1", Id = 1 }, new AnimalSpecimen { AnimalName = "Lion2", Id = 1 } });

            var okResult = _controller.GetSingleAnimalSpecimen(id);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<NotFoundObjectResult>(okResult);

            var animalSpecimensResult = Assert.IsType<object>(okObjectResult.Value);
        }

        [Fact]
        public void AddAnimalSpecimen_CallWithValidArgs_ReturnedAddedAnimalSpecimen()
        {
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 2 };

            animalTypeMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalType>>(), null))
                .Returns(new List<AnimalType> { new AnimalType { Id = 3, TypeName = "TestName", TypeCategoryId = 2 } });

            animalSpecimenMock.Setup(repo => repo.InsertOne(It.IsAny<AnimalSpecimen>()))
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
        public void AddAnimalSpecimen_CallWithWrongAnimalType_ReturnedBadRequest()
        {
            AnimalSpecimen animalSpecimen = new AnimalSpecimen { AnimalName = "Lion", Id = 1, TypeId = 3 };

            animalTypeMock.Setup(repo => repo.Find(It.IsAny<FilterDefinition<AnimalType>>(), null))
                .Returns(new List<AnimalType> {  });


            var okResult = _controller.AddAnimalSpecimen(animalSpecimen);
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<BadRequestObjectResult>(okResult);
             

        }



    }

}
