using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Controllers;
using zoo_manager_backend.Services;
using zoo_manager_backend.Models;
using zoo_manager_backend.Exceptions;

using Moq;
using Xunit;

namespace zoo_manager_tests
{

    public class AnimalSpecimenControllerTests
    {
        private readonly Mock<IAnimalSpecimenService> animalSpecimenServiceMock;
        private readonly AnimalSpecimenController _controller;


        public AnimalSpecimenControllerTests()
        {
            animalSpecimenServiceMock = new Mock<IAnimalSpecimenService>();
            _controller = new AnimalSpecimenController(animalSpecimenServiceMock.Object);
        }


        [Fact]
        public void GetAllAnimalSpecimens_ReturnValidData_RetrunOkObjectResult()
        {
            animalSpecimenServiceMock.Setup(serwis => serwis.GetAll())
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
        public void GetSingleAnimalSpecimen_GetInvalidData_ReturnNotFoundObject()
        {
            animalSpecimenServiceMock.Setup(serwis => serwis.Get(It.IsAny<int>()))
              .Throws<ItemNotFoundException>();
            // act
            var okResult = _controller.GetSingleAnimalSpecimen(It.IsAny<int>());
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<NotFoundResult>(okResult);

        }
        [Fact]
        public void GetSingleAnimalSpecimen_GetValidData_ReturnOkResultObject()
        {
            animalSpecimenServiceMock.Setup(serwis => serwis.Get(It.IsAny<int>()))
              .Returns(new AnimalSpecimen { });
            // act
            var okResult = _controller.GetSingleAnimalSpecimen(It.IsAny<int>());
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);

        }

        [Fact]
        public void AddAnimalSpecimen_GetValidData_ReturnOkResultObject()
        {
            animalSpecimenServiceMock.Setup(serwis => serwis.Add(It.IsAny<AnimalSpecimen>()))
              .Returns(new AnimalSpecimen { });
            // act
            var okResult = _controller.AddAnimalSpecimen(It.IsAny<AnimalSpecimen>());
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void AddAnimalSpecimen_GetInvalidData_ReturnBadRequestObject()
        {
            animalSpecimenServiceMock.Setup(serwis => serwis.Add(It.IsAny<AnimalSpecimen>()))
              .Throws<RequiredItemNotExistsException>();
            // act
            var okResult = _controller.AddAnimalSpecimen(It.IsAny<AnimalSpecimen>());
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<BadRequestObjectResult>(okResult);
        }

        [Fact]
        public void DeleteAnimalSpecimen_GetInvalidData_ReturnNotFoundObject()
        {
            animalSpecimenServiceMock.Setup(serwis => serwis.Delete(It.IsAny<int>()))
              .Throws<ItemNotFoundException>();
            // act
            var okResult = _controller.DeleteAnimalSpecimen(It.IsAny<int>());
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<NotFoundResult>(okResult);
        }


        [Fact]
        public void DeleteAnimalSpecimen_GetValidData_ReturnOkObject()
        {
            animalSpecimenServiceMock.Setup(serwis => serwis.Delete(It.IsAny<int>()))
              .Returns((AnimalSpecimen)null);
            // act
            var okResult = _controller.DeleteAnimalSpecimen(It.IsAny<int>());
            Assert.NotNull(okResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
