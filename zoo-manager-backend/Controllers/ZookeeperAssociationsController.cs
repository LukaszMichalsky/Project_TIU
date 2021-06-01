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
    public class ZookeeperAssociationsController : ControllerBase {
        private readonly MongoService<ZookeeperAssociation> zookeeperAssociationService;

        public ZookeeperAssociationsController(MongoService<ZookeeperAssociation> zookeeperAssociationService) {
            this.zookeeperAssociationService = zookeeperAssociationService;
            zookeeperAssociationService.CollectionNamespace = "zookeeper-associations";
        }

        [HttpGet]
        public IActionResult GetAllZookeeperAssociations() {
            // Find all zookeeper associations
            return Ok(zookeeperAssociationService.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().Empty));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleZookeeperAssociation(int id) {
            try {
                // Find selected zookeeper association
                return Ok(zookeeperAssociationService.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.Id == id)).Single());
            } catch {
                // Return empty object
                return Ok(new object { });
            }
        }

        [HttpPost]
        public IActionResult AddZookeeperAssociation([FromBody] ZookeeperAssociation newZookeeperAssociation) {
            int availableIndex = zookeeperAssociationService.GetAvailableId();

            // Return added zookeeper association
            return Ok(zookeeperAssociationService.InsertOne(new ZookeeperAssociation() {
                Id = availableIndex,
                AnimalTypeId = newZookeeperAssociation.AnimalTypeId,
                TypeZookeeperId = newZookeeperAssociation.TypeZookeeperId
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteZookeeperAssociation(int id) {
            // Find zookeeper association with given ID
            ZookeeperAssociation selectedZookeeperAssociation = zookeeperAssociationService.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.Id == id)).SingleOrDefault();

            if (selectedZookeeperAssociation == null) {
                return BadRequest("Zookeper association not exists");
            }

            // Return deleted zookeeper association
            return Ok(zookeeperAssociationService.FindOneAndDelete(new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.Id == id)));
        }
    }
}
