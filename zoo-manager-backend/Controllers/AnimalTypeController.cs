using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AnimalTypeController : ControllerBase {
        private readonly IAnimalTypeService animalTypeService;

        public AnimalTypeController(IAnimalTypeService animalTypeService) {
            this.animalTypeService = animalTypeService;
        }

        [HttpGet]
        public IActionResult GetAllAnimalTypes() {
            return Ok(animalTypeService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleAnimalType(int id) {
            try {
                return Ok(animalTypeService.Get(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddAnimalType([FromBody] AnimalType newAnimalType) {
            try {
                return Ok(animalTypeService.Add(newAnimalType));
            } catch (RequiredItemNotExistsException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteAnimalType(int id) {
            try {
                return Ok(animalTypeService.Delete(id));
            } catch (AssociationExistsException e) {
                return Conflict(e.Message);
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }
    }
}
