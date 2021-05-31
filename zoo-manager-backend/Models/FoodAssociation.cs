using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zoo_manager_backend.Models {
    public class FoodAssociation : MongoModel {
        [BsonElement("animal_type_id")]
        public int AnimalTypeId { get; set; }

        [BsonElement("food_id")]
        public int FoodId { get; set; }
    }
}
