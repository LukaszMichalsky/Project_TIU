using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ZookeepersController : ControllerBase {
        private readonly MongoService<AnimalType> animalTypeService;
        private readonly MongoService<Zookeeper> zookeeperService;

        public ZookeepersController(MongoService<AnimalType> animalTypeService, MongoService<Zookeeper> zookeeperService) {
            this.animalTypeService = animalTypeService;
            animalTypeService.CollectionNamespace = "animal-types";

            this.zookeeperService = zookeeperService;
            zookeeperService.CollectionNamespace = "zookeepers";
        }

        [HttpGet]
        public IActionResult GetAllZookeepers() {
            // Find all zookeepers
            return Ok(zookeeperService.Find(new FilterDefinitionBuilder<Zookeeper>().Empty));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleZookeeper(int id) {
            try {
                // Find selected zookeeper
                return Ok(zookeeperService.Find(new FilterDefinitionBuilder<Zookeeper>().Where(zookeeper => zookeeper.Id == id)).Single());
            } catch {
                // Return empty object
                return Ok(new object {});
            }
        }

        [HttpPost]
        public IActionResult AddZookeeper([FromBody] Zookeeper newZookeeper) {
            int availableIndex = zookeeperService.GetAvailableId();

            // Return added zookeeper
            return Ok(zookeeperService.InsertOne(new Zookeeper() {
                Id = availableIndex,
                ZookeeperName = newZookeeper.ZookeeperName,
                ZookeeperSurname = newZookeeper.ZookeeperSurname,
                ZookeeperPhoneNumber = newZookeeper.ZookeeperPhoneNumber
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteZookeeper(int id) {
            // Find zookeeper with given ID
            Zookeeper selectedZookeeper = zookeeperService.Find(new FilterDefinitionBuilder<Zookeeper>().Where(zookeeper => zookeeper.Id == id)).SingleOrDefault();

            if (selectedZookeeper == null) {
                return BadRequest("Zookeeper not exists");
            }

            // Find animal types with given zookeeper ID
            List<AnimalType> associatedAnimalTypes = animalTypeService.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.TypeZookeeperId == id));

            if (associatedAnimalTypes.Count > 0) {
                return Conflict("Zookeeper has associated animal types");
            }

            // Return deleted zookeeper
            return Ok(zookeeperService.FindOneAndDelete(new FilterDefinitionBuilder<Zookeeper>().Where(zookeeper => zookeeper.Id == id)));
        }
    }
}
