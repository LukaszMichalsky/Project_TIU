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
        private readonly MongoService<AnimalType> animalTypeService;
        private readonly MongoService<Food> foodService;
        private readonly MongoService<FoodAssociation> foodAssociationService;

        public FoodAssociationsController(MongoService<AnimalType> animalTypeService, MongoService<Food> foodService, MongoService<FoodAssociation> foodAssociationService) {
            this.animalTypeService = animalTypeService;
            this.foodService = foodService;
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

            // Ensure added animal type exists
            try {
                AnimalType addedType = animalTypeService.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == newFoodAssociation.AnimalTypeId)).Single();
            } catch {
                return BadRequest("Selected animal type not exists");
            }

            // Ensure added food item exists
            try {
                Food addedFood = foodService.Find(new FilterDefinitionBuilder<Food>().Where(food => food.Id == newFoodAssociation.FoodId)).Single();
            } catch {
                return BadRequest("Selected food item not exists");
            }

            // Find potential duplications
            FoodAssociation selectedAssociation = foodAssociationService.Find(new FilterDefinitionBuilder<FoodAssociation>().And(
                new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.AnimalTypeId == newFoodAssociation.AnimalTypeId),
                new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.FoodId == newFoodAssociation.FoodId)
            )).SingleOrDefault();

            if (selectedAssociation != null) {
                return BadRequest("Food association duplicate found");
            }

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
