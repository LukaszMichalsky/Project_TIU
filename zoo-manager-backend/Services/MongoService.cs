using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using zoo_manager_backend.Models;

namespace zoo_manager_backend.Services {
    public class MongoService<T> where T : MongoModel {
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

        public MongoService(MongoClient client) {
            database = client.GetDatabase("db");
            collectionName = string.Join('-', Regex.Matches(typeof(T).Name, @"[A-Z][a-z]*|[a-z]+|\d+")).ToLower();

            System.Diagnostics.Debug.WriteLine($"Generated collection name '{collectionName}' from service type '{typeof(T).Name}'");
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

        public T FindOneAndDelete(FilterDefinition<T> filter, FindOneAndDeleteOptions<T, T> options = null) {
            return collection.FindOneAndDelete(filter, options);
        }
    }
}
