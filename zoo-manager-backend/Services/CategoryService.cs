using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Repositories;

namespace zoo_manager_backend.Services {
    public class CategoryService {
        private readonly MongoRepository<AnimalType> animalTypeRepository;
        private readonly MongoRepository<Category> categoryRepository;

        public CategoryService(MongoRepository<AnimalType> animalTypeRepository, MongoRepository<Category> categoryRepository) {
            this.animalTypeRepository = animalTypeRepository;
            this.categoryRepository = categoryRepository;
        }

        public List<Category> GetAll() {
            // Find all items
            return categoryRepository.Find(new FilterDefinitionBuilder<Category>().Empty);
        }

        public Category Get(int id) {
            try {
                // Find single item
                return categoryRepository.Find(new FilterDefinitionBuilder<Category>().Where(category => category.Id == id)).Single();
            } catch {
                throw new ItemNotFoundException();
            }
        }

        public Category Add(Category category) {
            int newID = categoryRepository.GetAvailableId();

            // Add item to database
            return categoryRepository.InsertOne(new Category() {
                CategoryName = category.CategoryName,
                Id = newID
            });
        }

        public Category Delete(int id) {
            // Search for bindings
            if (animalTypeRepository.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.TypeCategoryId == id)).Count > 0) {
                throw new AssociationExistsException("This type has already associated categories");
            }

            Category deletedCategory = categoryRepository.FindOneAndDelete(new FilterDefinitionBuilder<Category>().Where(category => category.Id == id));

            // Find deleted item
            if (deletedCategory == null) {
                throw new ItemNotFoundException();
            }

            return deletedCategory;
        }
    }
}
