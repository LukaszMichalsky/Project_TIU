using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Exceptions;
using zoo_manager_backend.Models;
using zoo_manager_backend.Repositories;

namespace zoo_manager_backend.Services {
    public class ZookeeperAssociationService {
        private readonly MongoRepository<AnimalType> animalTypeRepository;
        private readonly MongoRepository<Zookeeper> zookeeperRepository;
        private readonly MongoRepository<ZookeeperAssociation> zookeeperAssociationRepository;

        public ZookeeperAssociationService(MongoRepository<AnimalType> animalTypeRepository, MongoRepository<Zookeeper> zookeeperRepository, MongoRepository<ZookeeperAssociation> zookeeperAssociationRepository) {
            this.animalTypeRepository = animalTypeRepository;
            this.zookeeperRepository = zookeeperRepository;
            this.zookeeperAssociationRepository = zookeeperAssociationRepository;
        }

        public List<ZookeeperAssociation> GetAll() {
            // Find all items
            return zookeeperAssociationRepository.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().Empty);
        }

        public ZookeeperAssociation Get(int id) {
            try {
                // Find single item
                return zookeeperAssociationRepository.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.Id == id)).Single();
            } catch {
                throw new ItemNotFoundException();
            }
        }

        public ZookeeperAssociation Add(ZookeeperAssociation zookeeperAssociation) {
            int newID = zookeeperAssociationRepository.GetAvailableId();

            // Search for relationships
            try {
                AnimalType boundType = animalTypeRepository.Find(new FilterDefinitionBuilder<AnimalType>().Where(type => type.Id == zookeeperAssociation.AnimalTypeId)).Single();
            } catch {
                throw new RequiredItemNotExistsException("Selected animal type not exists");
            }

            try {
                Zookeeper boundZookeeper = zookeeperRepository.Find(new FilterDefinitionBuilder<Zookeeper>().Where(zookeeper => zookeeper.Id == zookeeperAssociation.TypeZookeeperId)).Single();
            } catch {
                throw new RequiredItemNotExistsException("Selected zookeeper not exists");
            }

            // Find potential duplicates
            if (zookeeperAssociationRepository.Find(new FilterDefinitionBuilder<ZookeeperAssociation>().And(
                new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.AnimalTypeId == zookeeperAssociation.AnimalTypeId),
                new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.TypeZookeeperId == zookeeperAssociation.TypeZookeeperId)
            )).Count > 0) {
                throw new DuplicateFoundException("Zookeeper association duplicate found");
            }

            // Add item to database
            return zookeeperAssociationRepository.InsertOne(new ZookeeperAssociation() {
                Id = newID,
                AnimalTypeId = zookeeperAssociation.AnimalTypeId,
                TypeZookeeperId = zookeeperAssociation.TypeZookeeperId
            });
        }

        public ZookeeperAssociation Delete(int id) {
            ZookeeperAssociation deletedZookeeperAssociation = zookeeperAssociationRepository.FindOneAndDelete(new FilterDefinitionBuilder<ZookeeperAssociation>().Where(zookeeper => zookeeper.Id == id));

            // Find deleted item
            if (deletedZookeeperAssociation == null) {
                throw new ItemNotFoundException();
            }

            return deletedZookeeperAssociation;
        }
    }
}
