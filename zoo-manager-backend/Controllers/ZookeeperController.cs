using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ZookeeperController : ControllerBase {
        private readonly IZookeeperService zookeeperService;

        public ZookeeperController(IZookeeperService zookeeperService) {
            this.zookeeperService = zookeeperService;
        }

        [HttpGet]
        public IActionResult GetAllZookeepers() {
            return Ok(zookeeperService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleZookeeper(int id) {
            try {
                return Ok(zookeeperService.Get(id));
            } catch {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddZookeeper([FromBody] Zookeeper newZookeeper) {
            return Ok(zookeeperService.Add(newZookeeper));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteZookeeper(int id) {
            try {
                return Ok(zookeeperService.Delete(id));
            } catch (AssociationExistsException e) {
                return Conflict(e.Message);
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }
    }
}
