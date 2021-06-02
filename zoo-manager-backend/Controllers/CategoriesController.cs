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
    public class CategoriesController : ControllerBase {
        private readonly MongoService<AnimalType> animalTypeService;
        private readonly MongoService<Category> categoryService;

        public CategoriesController(MongoService<Category> categoryService, MongoService<AnimalType> animalTypeService) {
            this.categoryService = categoryService;
            this.animalTypeService = animalTypeService;
        }

        [HttpGet]
        public IActionResult GetAllCategories() {
            // Find all categories
            return Ok(categoryService.Find(new FilterDefinitionBuilder<Category>().Empty));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleCategory(int id) {
            try {
                // Find selected category
                return Ok(categoryService.Find(new FilterDefinitionBuilder<Category>().Where(category => category.Id == id)).Single());
            } catch {
                // Return empty object
                return Ok(new object {});
            }
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category newCategory) {
            int availableIndex = categoryService.GetAvailableId();

            // Return added category
            return Ok(categoryService.InsertOne(new Category() {
                CategoryName = newCategory.CategoryName,
                Id = availableIndex
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategory(int id) {
            // Find category with given ID
            Category selectedCategory = categoryService.Find(new FilterDefinitionBuilder<Category>().Where(category => category.Id == id)).SingleOrDefault();

            if (selectedCategory == null) {
                return BadRequest("Category not exists");
            }

            // Find animal types with given category ID
            List<AnimalType> associatedAnimalTypes = animalTypeService.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.TypeCategoryId == selectedCategory.Id));

            if (associatedAnimalTypes.Count > 0) {
                return Conflict("Category has associated animal types");
            }

            // Return deleted category
            return Ok(categoryService.FindOneAndDelete(new FilterDefinitionBuilder<Category>().Where(category => category.Id == id)));
        }
    }
}
