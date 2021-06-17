using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Repositories;

namespace zoo_manager_backend.Services {
    public class FoodService {
        private readonly MongoRepository<Food> foodRepository;
        private readonly MongoRepository<FoodAssociation> foodAssociationRepository;

        public FoodService(MongoRepository<Food> foodRepository, MongoRepository<FoodAssociation> foodAssociationRepository) {
            this.foodRepository = foodRepository;
            this.foodAssociationRepository = foodAssociationRepository;
        }

        public List<Food> GetAll() {
            // Find all items
            return foodRepository.Find(new FilterDefinitionBuilder<Food>().Empty);
        }

        public Food Get(int id) {
            try {
                // Find single item
                return foodRepository.Find(new FilterDefinitionBuilder<Food>().Where(food => food.Id == id)).Single();
            } catch {
                throw new ItemNotFoundException();
            }
        }

        public Food Add(Food foodItem) {
            int newID = foodRepository.GetAvailableId();

            // Add item to database
            return foodRepository.InsertOne(new Food() {
                FoodBuyPrice = foodItem.FoodBuyPrice,
                FoodName = foodItem.FoodName,
                FoodStorageQuantity = foodItem.FoodStorageQuantity,
                Id = newID
            });
        }

        public Food Delete(int id) {
            // Search for bindings
            if (foodAssociationRepository.Find(new FilterDefinitionBuilder<FoodAssociation>().Where(foodAssociation => foodAssociation.FoodId == id)).Count > 0) {
                throw new AssociationExistsException("This food item has already associated food bindings");
            }

            Food deletedFoodItem = foodRepository.FindOneAndDelete(new FilterDefinitionBuilder<Food>().Where(food => food.Id == id));

            // Find deleted item
            if (deletedFoodItem == null) {
                throw new ItemNotFoundException();
            }

            return deletedFoodItem;
        }
    }
}
