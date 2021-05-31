using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zoo_manager_backend.Models {
    public class Zookeeper : MongoModel {
        [BsonElement("zookeeper_name")]
        public string ZookeeperName { get; set; }

        [BsonElement("zookeeper_surname")]
        public string ZookeeperSurname { get; set; }

        [BsonElement("zookeeper_phone_number")]
        public string ZookeeperPhoneNumber { get; set; }
    }
}
