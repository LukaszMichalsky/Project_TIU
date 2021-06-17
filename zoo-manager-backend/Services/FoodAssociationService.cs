using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Repositories;

namespace zoo_manager_backend.Services {
    public class FoodAssociationService {
        private readonly MongoRepository<AnimalType> animalTypeRepository;
        private readonly MongoRepository<Food> foodRepository;
        private readonly MongoRepository<FoodAssociation> foodAssociationRepository;

        public FoodAssociationService(MongoRepository<AnimalType> animalTypeRepository, MongoRepository<Food> foodRepository, MongoRepository<FoodAssociation> foodAssociationRepository) {
            this.animalTypeRepository = animalTypeRepository;
            this.foodRepository = foodRepository;
            this.foodAssociationRepository = foodAssociationRepository;
        }

        public List<FoodAssociation> GetAll() {
            // Find all items
            return foodAssociationRepository.Find(new FilterDefinitionBuilder<FoodAssociation>().Empty);
        }

        public FoodAssociation Get(int id) {
            try {
                // Find single item
                return foodAssociationRepository.Find(new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.Id == id)).Single();
            } catch {
                throw new ItemNotFoundException();
            }
        }

        public FoodAssociation Add(FoodAssociation foodAssociation) {
            int newID = foodAssociationRepository.GetAvailableId();

            // Search for relationships
            try {
                AnimalType boundType = animalTypeRepository.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == foodAssociation.AnimalTypeId)).Single();
            } catch {
                throw new RequiredItemNotExistsException("Selected animal type not exists");
            }

            try {
                Food boundFood = foodRepository.Find(new FilterDefinitionBuilder<Food>().Where(food => food.Id == foodAssociation.FoodId)).Single();
            } catch {
                throw new RequiredItemNotExistsException("Selected food item not exists");
            }

            // Find potential duplicates
            if (foodAssociationRepository.Find(new FilterDefinitionBuilder<FoodAssociation>().And(
                new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.AnimalTypeId == foodAssociation.AnimalTypeId),
                new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.FoodId == foodAssociation.FoodId)
            )).Count > 0) {
                throw new DuplicateFoundException("Food association duplicate found");
            }

            // Add item to database
            return foodAssociationRepository.InsertOne(new FoodAssociation() {
                Id = newID,
                AnimalTypeId = foodAssociation.AnimalTypeId,
                FoodId = foodAssociation.FoodId
            });
        }

        public FoodAssociation Delete(int id) {
            FoodAssociation deletedFoodAssociation = foodAssociationRepository.FindOneAndDelete(new FilterDefinitionBuilder<FoodAssociation>().Where(food => food.Id == id));

            // Find deleted item
            if (deletedFoodAssociation == null) {
                throw new ItemNotFoundException();
            }

            return deletedFoodAssociation;
        }
    }
}
