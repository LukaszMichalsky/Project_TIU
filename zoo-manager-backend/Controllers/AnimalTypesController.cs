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
    public class AnimalTypesController : ControllerBase {
        private readonly MongoService<AnimalSpecimen> animalSpecimenService;
        private readonly MongoService<AnimalType> animalTypeService;
        private readonly MongoService<FoodAssociation> foodAssociationService;

        public AnimalTypesController(MongoService<AnimalSpecimen> animalSpecimenService, MongoService<AnimalType> animalTypeService, MongoService<FoodAssociation> foodAssociationService) {
            this.animalSpecimenService = animalSpecimenService;
            animalSpecimenService.CollectionNamespace = "animal-specimens";

            this.animalTypeService = animalTypeService;
            animalTypeService.CollectionNamespace = "animal-types";

            this.foodAssociationService = foodAssociationService;
            foodAssociationService.CollectionNamespace = "food-associations";
        }

        [HttpGet]
        public IActionResult GetAllAnimalTypes() {
            // Find all animal types
            return Ok(animalTypeService.Find(new FilterDefinitionBuilder<AnimalType>().Empty));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleAnimalType(int id) {
            try {
                // Find selected animal type
                return Ok(animalTypeService.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == id)).Single());
            } catch {
                // Return empty object
                return Ok(new object {});
            }
        }

        [HttpPost]
        public IActionResult AddAnimalType([FromBody] AnimalType newAnimalType) {
            int availableIndex = animalTypeService.GetAvailableId();

            // Return added animal type
            return Ok(animalTypeService.InsertOne(new AnimalType() {
                Id = availableIndex,
                TypeCategoryId = newAnimalType.TypeCategoryId,
                TypeName = newAnimalType.TypeName
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteAnimalType(int id) {
            // Find animal type with given ID
            AnimalType selectedAnimalType = animalTypeService.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == id)).SingleOrDefault();

            if (selectedAnimalType == null) {
                return BadRequest("Animal type not exists");
            }

            // Find animal specimens with given animal type ID
            List<AnimalSpecimen> associatedAnimalSpecimens = animalSpecimenService.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Where(type => type.TypeId == id));

            if (associatedAnimalSpecimens.Count > 0) {
                return Conflict("Type has associated animal specimens");
            }

            // Find food associations with given animal type ID
            List<FoodAssociation> associatedFoodItems = foodAssociationService.Find(new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.AnimalTypeId == id));

            if (associatedFoodItems.Count > 0) {
                return Conflict("Type has associated food items");
            }

            // Return deleted animal type
            return Ok(animalTypeService.FindOneAndDelete(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == id)));
        }
    }
}
