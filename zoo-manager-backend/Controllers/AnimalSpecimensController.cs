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
    public class AnimalSpecimensController : ControllerBase {
        private readonly MongoService<AnimalSpecimen> animalSpecimenService;

        public AnimalSpecimensController(MongoService<AnimalSpecimen> animalSpecimenService) {
            this.animalSpecimenService = animalSpecimenService;
            animalSpecimenService.CollectionNamespace = "animal-specimens";
        }

        [HttpGet]
        public IActionResult GetAllAnimalSpecimens() {
            // Find all animal specimens
            return Ok(animalSpecimenService.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Empty));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleAnimalSpecimen(int id) {
            try {
                // Find selected animal specimen
                return Ok(animalSpecimenService.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Where(specimen => specimen.Id == id)).Single());
            } catch {
                // Return empty object
                return Ok(new object {});
            }
        }

        [HttpPost]
        public IActionResult AddAnimalSpecimen([FromBody] AnimalSpecimen newAnimalSpecimen) {
            int availableIndex = animalSpecimenService.GetAvailableId();

            // Return added animal specimen
            return Ok(animalSpecimenService.InsertOne(new AnimalSpecimen() {
                Id = availableIndex,
                AnimalName = newAnimalSpecimen.AnimalName,
                TypeId = newAnimalSpecimen.TypeId
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteAnimalSpecimen(int id) {
            // Find animal specimen with given ID
            AnimalSpecimen selectedAnimalSpecimen = animalSpecimenService.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Where(specimen => specimen.Id == id)).SingleOrDefault();

            if (selectedAnimalSpecimen == null) {
                return BadRequest("Animal specimen not exists");
            }

            // Return deleted animal specimen
            return Ok(animalSpecimenService.FindOneAndDelete(new FilterDefinitionBuilder<AnimalSpecimen>().Where(specimen => specimen.Id == id)));
        }
    }
}
