using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public class MongoService<T> where T : MongoModel {
        private readonly IMongoDatabase database;
        private IMongoCollection<T> collection;
        private string collectionNamespace = "default";

        public string CollectionNamespace {
            get {
                return collectionNamespace;
            }

            set {
                collectionNamespace = value;
                collection = database.GetCollection<T>(collectionNamespace);
            }
        }

        public MongoService(MongoClient client) {
            database = client.GetDatabase("db");
        }

        public int GetAvailableId() {
            if (collection.CountDocuments(model => true) > 0) {
                return (collection.Find(model => true).SortByDescending(model => model.Id).Limit(1).FirstOrDefault().Id) + 1;
            } else {
                return 1;
            }
        }

        public List<T> Find(FilterDefinition<T> filter, FindOptions options = null) {
            return collection.Find(filter, options).ToList();
        }

        public T InsertOne(T newElement) {
            try {
                collection.InsertOne(newElement);
                return newElement;
            } catch {
                return null;
            }
        }

        public T DeleteOne(FilterDefinition<T> filter) {
            return collection.FindOneAndDelete(filter);
        }
    }
}
