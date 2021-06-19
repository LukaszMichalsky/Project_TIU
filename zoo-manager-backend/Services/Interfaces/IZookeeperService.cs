using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public interface IZookeeperService {
        public List<Zookeeper> GetAll();
        public Zookeeper Get(int id);
        public Zookeeper Add(Zookeeper zookeeper);
        public Zookeeper Delete(int id);
    }
}
