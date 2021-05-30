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
    public class CategoriesController : ControllerBase {
        private readonly MongoService<Category> mongoService;

        public CategoriesController(MongoService<Category> mongoService) {
            this.mongoService = mongoService;
            mongoService.CollectionNamespace = "categories";
        }

        [HttpGet]
        [Route("get")]
        public List<Category> GetCategories() {
            return mongoService.Find((FilterDefinition<Category>) (category => true));
        }

        [HttpGet]
        [Route("get/{id}")]
        public Category GetCategory(int id) {
            return mongoService.Find(new FilterDefinitionBuilder<Category>().Where(category => category.Id == id)).SingleOrDefault();
        }

        [HttpPost]
        [Route("add")]
        public Category AddCategory([FromBody] Category newCategory) {
            int availableIndex = mongoService.GetAvailableId();

            return mongoService.InsertOne(new Category() {
                CategoryName = newCategory.CategoryName,
                Id = availableIndex
            });
        }
    }
}
