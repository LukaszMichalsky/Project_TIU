using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zoo_manager_backend.Models {
    public class Food : MongoModel {
        [BsonElement("food_name")]
        public string FoodName { get; set; }

        [BsonElement("food_buy_price")]
        public decimal FoodBuyPrice { get; set; }

        [BsonElement("food_storage_quantity")]
        public int FoodStorageQuantity { get; set; }
    }
}
