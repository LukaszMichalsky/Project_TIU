using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public interface ICategoryService {
        public List<Category> GetAll();
        public Category Get(int id);
        public Category Add(Category category);
        public Category Delete(int id);
    }
}
