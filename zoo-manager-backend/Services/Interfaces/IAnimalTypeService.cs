using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public interface IAnimalTypeService {
        public List<AnimalType> GetAll();
        public AnimalType Get(int id);
        public AnimalType Add(AnimalType animalType);
        public AnimalType Delete(int id);
    }
}
