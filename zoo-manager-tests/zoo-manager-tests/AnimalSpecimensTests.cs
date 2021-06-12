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
            animalSpecimenMock = new Mock<MongoService<AnimalSpecimen>>(new MongoClient(Config.DB_CONNECTION_STRING)); //todo: mock
            animalTypeMock = new Mock<MongoService<AnimalType>>(new MongoClient(Config.DB_CONNECTION_STRING)); //todo: mock
            _controller = new AnimalSpecimensController(animalSpecimenMock.Object, animalTypeMock.Object);
        }

        [Fact]
        public void GetAllAnimalSpecimens_CallAction_GetListOfAnimalSpecimens()
        {
            var result = _controller.GetAllAnimalSpecimens();
            Assert.IsType<OkObjectResult>(result);
        }

    }

}
