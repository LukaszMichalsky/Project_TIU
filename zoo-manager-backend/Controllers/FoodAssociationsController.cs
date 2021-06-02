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
    public class FoodAssociationsController : ControllerBase {
        private readonly MongoService<FoodAssociation> foodAssociationService;

        public FoodAssociationsController(MongoService<FoodAssociation> foodAssociationService) {
            this.foodAssociationService = foodAssociationService;
        }

        [HttpGet]
        public IActionResult GetAllFoodAssociations() {
            // Find all food associations
            return Ok(foodAssociationService.Find(new FilterDefinitionBuilder<FoodAssociation>().Empty));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleFoodAssociation(int id) {
            try {
                // Find selected food association
                return Ok(foodAssociationService.Find(new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.Id == id)).Single());
            } catch {
                // Return empty object
                return Ok(new object {});
            }
        }

        [HttpPost]
        public IActionResult AddFoodAssociation([FromBody] FoodAssociation newFoodAssociation) {
            int availableIndex = foodAssociationService.GetAvailableId();

            // Return added food association
            return Ok(foodAssociationService.InsertOne(new FoodAssociation() {
                Id = availableIndex,
                AnimalTypeId = newFoodAssociation.AnimalTypeId,
                FoodId = newFoodAssociation.FoodId
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFoodAssociation(int id) {
            // Find food association with given ID
            FoodAssociation selectedFoodAssociation = foodAssociationService.Find(new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.Id == id)).SingleOrDefault();

            if (selectedFoodAssociation == null) {
                return BadRequest("Food association not exists");
            }

            // Return deleted food association
            return Ok(foodAssociationService.FindOneAndDelete(new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.Id == id)));
        }
    }
}
