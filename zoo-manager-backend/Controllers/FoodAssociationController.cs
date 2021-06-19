using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FoodAssociationController : ControllerBase {
        private readonly IFoodAssociationService foodAssociationService;

        public FoodAssociationController(IFoodAssociationService foodAssociationService) {
            this.foodAssociationService = foodAssociationService;
        }

        [HttpGet]
        public IActionResult GetAllFoodAssociations() {
            return Ok(foodAssociationService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleFoodAssociation(int id) {
            try {
                return Ok(foodAssociationService.Get(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddFoodAssociation([FromBody] FoodAssociation newFoodAssociation) {
            try {
                return Ok(foodAssociationService.Add(newFoodAssociation));
            } catch (RequiredItemNotExistsException e) {
                return BadRequest(e.Message);
            } catch (DuplicateFoundException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFoodAssociation(int id) {
            try {
                return Ok(foodAssociationService.Delete(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }
    }
}
