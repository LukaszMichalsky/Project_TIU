using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Repositories;

namespace zoo_manager_backend.Services {
    public class ZookeeperService : IZookeeperService {
        private readonly MongoRepository<Zookeeper> zookeeperRepository;
        private readonly MongoRepository<ZookeeperAssociation> zookeeperAssociationsRepository;

        public ZookeeperService(MongoRepository<Zookeeper> zookeeperRepository, MongoRepository<ZookeeperAssociation> zookeeperAssociationsRepository) {
            this.zookeeperRepository = zookeeperRepository;
            this.zookeeperAssociationsRepository = zookeeperAssociationsRepository;
        }

        public List<Zookeeper> GetAll() {
            // Find all items
            return zookeeperRepository.Find(new FilterDefinitionBuilder<Zookeeper>().Empty);
        }

        public Zookeeper Get(int id) {
            try {
                // Find single item
                return zookeeperRepository.Find(new FilterDefinitionBuilder<Zookeeper>().Where(zookeeper => zookeeper.Id == id)).Single();
            } catch {
                throw new ItemNotFoundException();
            }
        }

        public Zookeeper Add(Zookeeper zookeeper) {
            int newID = zookeeperRepository.GetAvailableId();

            // Add item to database
            return zookeeperRepository.InsertOne(new Zookeeper() {
                Id = newID,
                ZookeeperName = zookeeper.ZookeeperName,
                ZookeeperSurname = zookeeper.ZookeeperSurname,
                ZookeeperPhoneNumber = zookeeper.ZookeeperPhoneNumber
            });
        }

        public Zookeeper Delete(int id) {
            // Search for bindings
            if (zookeeperAssociationsRepository.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeperAssociation => zookeeperAssociation.TypeZookeeperId == id)).Count > 0) {
                throw new AssociationExistsException("This zookeeper has already associated zookeeper bindings");
            }

            Zookeeper deletedZookeeper = zookeeperRepository.FindOneAndDelete(new FilterDefinitionBuilder<Zookeeper>().Where(zookeeper => zookeeper.Id == id));

            // Find deleted item
            if (deletedZookeeper == null) {
                throw new ItemNotFoundException();
            }

            return deletedZookeeper;
        }
    }
}
