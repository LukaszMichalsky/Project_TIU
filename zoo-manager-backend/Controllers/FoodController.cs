using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FoodController : ControllerBase {
        private readonly IFoodService foodService;

        public FoodController(IFoodService foodService) {
            this.foodService = foodService;
        }

        [HttpGet]
        public IActionResult GetAllFood() {
            return Ok(foodService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleFood(int id) {
            try {
                return Ok(foodService.Get(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddFood([FromBody] Food newFood) {
            return Ok(foodService.Add(newFood));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFood(int id) {
            try {
                return Ok(foodService.Delete(id));
            } catch (AssociationExistsException e) {
                return Conflict(e.Message);
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }
    }
}
