using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AnimalSpecimenController : ControllerBase
    {
        private readonly IAnimalSpecimenService animalSpecimenService;

        public AnimalSpecimenController(IAnimalSpecimenService animalSpecimenService)
        {
            this.animalSpecimenService = animalSpecimenService;
        }

        [HttpGet]
        public IActionResult GetAllAnimalSpecimens() {
            return Ok(animalSpecimenService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleAnimalSpecimen(int id) {
            try {
                return Ok(animalSpecimenService.Get(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddAnimalSpecimen([FromBody] AnimalSpecimen newAnimalSpecimen) {
            try {
                return Ok(animalSpecimenService.Add(newAnimalSpecimen));
            } catch (RequiredItemNotExistsException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteAnimalSpecimen(int id) {
            try {
                return Ok(animalSpecimenService.Delete(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }
    }
}
