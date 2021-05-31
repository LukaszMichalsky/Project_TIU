using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zoo_manager_backend.Models {
    public class AnimalType : MongoModel {
        [BsonElement("type_name")]
        public string TypeName { get; set; }

        [BsonElement("type_category_id")]
        public int TypeCategoryId { get; set; }

        [BsonElement("type_zookeeper_id")]
        public int TypeZookeeperId { get; set; }
    }
}
