using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Repositories;

namespace zoo_manager_backend.Services {
    public class AnimalSpecimenService : IAnimalSpecimenService {
        private readonly MongoRepository<AnimalSpecimen> animalSpecimenRepository;
        private readonly MongoRepository<AnimalType> animalTypeRepository;

        public AnimalSpecimenService(MongoRepository<AnimalSpecimen> animalSpecimenRepository, MongoRepository<AnimalType> animalTypeRepository) {
            this.animalSpecimenRepository = animalSpecimenRepository;
            this.animalTypeRepository = animalTypeRepository;
        }

        public List<AnimalSpecimen> GetAll() {
            // Find all items
            return animalSpecimenRepository.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Empty);
        }

        public AnimalSpecimen Get(int id) {
            try {
                // Find single item
                return animalSpecimenRepository.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Where(specimen => specimen.Id == id)).Single();
            } catch {
                throw new ItemNotFoundException();
            }
        }

        public AnimalSpecimen Add(AnimalSpecimen animalSpecimen) {
            int newID = animalSpecimenRepository.GetAvailableId();

            // Search for relationships
            try {
                AnimalType boundType = animalTypeRepository.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == animalSpecimen.TypeId)).Single();
            } catch {
                throw new RequiredItemNotExistsException("Selected animal type not exists");
            }

            // Add item to database
            return animalSpecimenRepository.InsertOne(new AnimalSpecimen() {
                Id = newID,
                AnimalName = animalSpecimen.AnimalName,
                TypeId = animalSpecimen.TypeId,
            });
        }

        public AnimalSpecimen Delete(int id) {
            AnimalSpecimen deletedAnimalSpecimen = animalSpecimenRepository.FindOneAndDelete(new FilterDefinitionBuilder<AnimalSpecimen>().Where(specimen => specimen.Id == id));

            // Find deleted item
            if (deletedAnimalSpecimen == null) {
                throw new ItemNotFoundException();
            }

            return deletedAnimalSpecimen;
        }
    }
}
