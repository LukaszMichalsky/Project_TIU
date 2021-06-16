using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using zoo_manager_backend.Models;

namespace zoo_manager_backend.Repositories {
    public class MongoRepository<T> where T : MongoModel {
        private readonly IMongoDatabase database;
        private IMongoCollection<T> collection;
        private string collectionName = "default";

        public string CollectionName {
            get {
                return collectionName;
            }

            set {
                collectionName = value;
                collection = database.GetCollection<T>(collectionName);
            }
        }

        public MongoRepository(MongoClient client) {
            database = client.GetDatabase("db");
            CollectionName = string.Join('-', Regex.Matches(typeof(T).Name, @"[A-Z][a-z]*|[a-z]+|\d+")).ToLower();

            System.Diagnostics.Debug.WriteLine($"Generated collection name '{CollectionName}' from service type '{typeof(T).Name}'");
        }

        virtual public int GetAvailableId() {
            if (collection.CountDocuments(model => true) > 0) {
                return (collection.Find(model => true).SortByDescending(model => model.Id).Limit(1).FirstOrDefault().Id) + 1;
            } else {
                return 1;
            }
        }

        virtual public List<T> Find(FilterDefinition<T> filter, FindOptions options = null) {
            return collection.Find(filter, options).ToList();
        }                                            

        virtual public T InsertOne(T newElement) {
            try {
                collection.InsertOne(newElement);
                return newElement;
            } catch {
                return null;
            }
        }

        virtual public T FindOneAndDelete(FilterDefinition<T> filter, FindOneAndDeleteOptions<T, T> options = null) {
            return collection.FindOneAndDelete(filter, options);
        }
    }
}
