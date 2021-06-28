using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Services;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories() {
            return Ok(categoryService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleCategory(int id) {
            try {
                return Ok(categoryService.Get(id));
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category newCategory) {
            return Ok(categoryService.Add(newCategory));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategory(int id) {
            try {
                return Ok(categoryService.Delete(id));
            } catch (AssociationExistsException e) {
                return Conflict(e.Message);
            } catch (ItemNotFoundException) {
                return NotFound();
            }
        }
    }
}
