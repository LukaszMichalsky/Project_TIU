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
    public class FoodController : ControllerBase {
        private readonly MongoService<Food> foodService;
        private readonly MongoService<FoodAssociation> foodAssociationService;

        public FoodController(MongoService<Food> foodService, MongoService<FoodAssociation> foodAssociationService) {
            this.foodService = foodService;
            foodService.CollectionNamespace = "food";

            this.foodAssociationService = foodAssociationService;
            foodAssociationService.CollectionNamespace = "food-associations";
        }

        [HttpGet]
        public IActionResult GetAllFood() {
            // Find all food items
            return Ok(foodService.Find(new FilterDefinitionBuilder<Food>().Empty));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleFood(int id) {
            try {
                // Find selected food item
                return Ok(foodService.Find(new FilterDefinitionBuilder<Food>().Where(food => food.Id == id)).Single());
            } catch {
                // Return empty object
                return Ok(new object { });
            }
        }

        [HttpPost]
        public IActionResult AddFood([FromBody] Food newFood) {
            int availableIndex = foodService.GetAvailableId();

            // Return added food item
            return Ok(foodService.InsertOne(new Food() {
                FoodBuyPrice = newFood.FoodBuyPrice,
                FoodName = newFood.FoodName,
                FoodStorageQuantity = newFood.FoodStorageQuantity,
                Id = availableIndex
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFood(int id) {
            // Find food item with given ID
            Food selectedFood = foodService.Find(new FilterDefinitionBuilder<Food>().Where(food => food.Id == id)).SingleOrDefault();

            if (selectedFood == null) {
                return BadRequest("Food item not exists");
            }

            // Find food associations with given food item ID
            List<FoodAssociation> associatedFoodItems = foodAssociationService.Find(new FilterDefinitionBuilder<FoodAssociation>().Where(association => association.FoodId == selectedFood.Id));

            if (associatedFoodItems.Count > 0) {
                return Conflict("Food item is already associated, delete food associations first");
            }

            // Return deleted food item
            return Ok(foodService.FindOneAndDelete(new FilterDefinitionBuilder<Food>().Where(food => food.Id == id)));
        }
    }
}
