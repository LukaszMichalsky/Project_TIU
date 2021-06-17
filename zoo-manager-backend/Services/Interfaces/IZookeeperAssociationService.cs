using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public interface IZookeeperAssociationService {
        public List<ZookeeperAssociation> GetAll();
        public ZookeeperAssociation Get(int id);
        public ZookeeperAssociation Add(ZookeeperAssociation zookeeperAssociation);
        public ZookeeperAssociation Delete(int id);
    }
}
