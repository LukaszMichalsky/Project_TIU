using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zoo_manager_backend.Models {
    public class MongoModel {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
    }
}