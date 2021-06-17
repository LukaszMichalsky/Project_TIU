using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public interface IFoodAssociationService {
        public List<FoodAssociation> GetAll();
        public FoodAssociation Get(int id);
        public FoodAssociation Add(FoodAssociation foodAssociation);
        public FoodAssociation Delete(int id);
    }
}
