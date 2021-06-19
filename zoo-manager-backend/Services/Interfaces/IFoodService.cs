using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public interface IFoodService {
        public List<Food> GetAll();
        public Food Get(int id);
        public Food Add(Food food);
        public Food Delete(int id);
    }
}
