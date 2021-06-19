using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public interface IAnimalSpecimenService {
        public List<AnimalSpecimen> GetAll();
        public AnimalSpecimen Get(int id);
        public AnimalSpecimen Add(AnimalSpecimen animalSpecimen);
        public AnimalSpecimen Delete(int id);
    }
}
