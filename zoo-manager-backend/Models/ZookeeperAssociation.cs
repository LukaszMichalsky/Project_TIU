using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zoo_manager_backend.Models {
    public class ZookeeperAssociation : MongoModel {
        [BsonElement("animal_type_id")]
        public int AnimalTypeId { get; set; }

        [BsonElement("type_zookeeper_id")]
        public int TypeZookeeperId { get; set; }
    }
}
