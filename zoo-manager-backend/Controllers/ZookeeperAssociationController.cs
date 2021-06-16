using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ZookeeperAssociationController : ControllerBase {
        private readonly ZookeeperAssociationService zookeeperAssociationService;

        public ZookeeperAssociationController(ZookeeperAssociationService zookeeperAssociationService) {
            this.zookeeperAssociationService = zookeeperAssociationService;
        }

        [HttpGet]
        public IActionResult GetAllZookeeperAssociations() {
            return Ok(zookeeperAssociationService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleZookeeperAssociation(int id) {
            try {
                return Ok(zookeeperAssociationService.Get(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddZookeeperAssociation([FromBody] ZookeeperAssociation newZookeeperAssociation) {
            try {
                return Ok(zookeeperAssociationService.Add(newZookeeperAssociation));
            } catch (RequiredItemNotExistsException e) {
                return BadRequest(e.Message);
            } catch (DuplicateFoundException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteZookeeperAssociation(int id) {
            try {
                return Ok(zookeeperAssociationService.Delete(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }
    }
}
