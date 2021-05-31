using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zoo_manager_backend.Models {
    public class AnimalSpecimen : MongoModel {
        [BsonElement("animal_name")]
        public string AnimalName { get; set; }

        [BsonElement("type_id")]
        public int TypeId { get; set; }
    }
}
