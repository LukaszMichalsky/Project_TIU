using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Repositories;

namespace zoo_manager_backend.Services {
    public class AnimalTypeService {
        private readonly MongoRepository<AnimalSpecimen> animalSpecimenRepository;
        private readonly MongoRepository<AnimalType> animalTypeRepository;
        private readonly MongoRepository<Category> categoryRepository;
        private readonly MongoRepository<FoodAssociation> foodAssociationRepository;
        private readonly MongoRepository<ZookeeperAssociation> zookeeperAssociationRepository;

        public AnimalTypeService(MongoRepository<AnimalSpecimen> animalSpecimenRepository, MongoRepository<AnimalType> animalTypeRepository, MongoRepository<Category> categoryRepository, MongoRepository<FoodAssociation> foodAssociationRepository, MongoRepository<ZookeeperAssociation> zookeeperAssociationRepository) {
            this.animalSpecimenRepository = animalSpecimenRepository;
            this.animalTypeRepository = animalTypeRepository;
            this.categoryRepository = categoryRepository;
            this.foodAssociationRepository = foodAssociationRepository;
            this.zookeeperAssociationRepository = zookeeperAssociationRepository;
        }

        public List<AnimalType> GetAll() {
            // Find all items
            return animalTypeRepository.Find(new FilterDefinitionBuilder<AnimalType>().Empty);
        }

        public AnimalType Get(int id) {
            try {
                // Find single item
                return animalTypeRepository.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == id)).Single();
            } catch {
                throw new ItemNotFoundException();
            }
        }

        public AnimalType Add(AnimalType animalType) {
            int newID = animalTypeRepository.GetAvailableId();

            // Search for relationships
            try {
                Category boundCategory = categoryRepository.Find(new FilterDefinitionBuilder<Category>().Where(category => category.Id == animalType.TypeCategoryId)).Single();
            } catch {
                throw new RequiredItemNotExistsException("Selected category not exists");
            }

            // Add item to database
            return animalTypeRepository.InsertOne(new AnimalType() {
                Id = newID,
                TypeCategoryId = animalType.TypeCategoryId,
                TypeName = animalType.TypeName
            });
        }

        public AnimalType Delete(int id) {
            // Search for bindings
            if (animalSpecimenRepository.Find(new FilterDefinitionBuilder<AnimalSpecimen>().Where(specimen => specimen.TypeId == id)).Count > 0) {
                throw new AssociationExistsException("This type has already associated animal specimens");
            }

            if (foodAssociationRepository.Find(new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.AnimalTypeId == id)).Count > 0) {
                throw new AssociationExistsException("This type has already associated food items");
            }

            if (zookeeperAssociationRepository.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.AnimalTypeId == id)).Count > 0) {
                throw new AssociationExistsException("This type has already associated zookeepers");
            }

            AnimalType deletedAnimalType = animalTypeRepository.FindOneAndDelete(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == id));

            // Find deleted item
            if (deletedAnimalType == null) {
                throw new ItemNotFoundException();
            }

            return deletedAnimalType;
        }
    }
}
