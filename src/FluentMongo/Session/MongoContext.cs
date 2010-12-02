using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMongo.Linq;
using MongoDB.Driver;
using MongoDB.Bson.DefaultSerializer;
using FluentMongo.Session.Conventions;
using FluentMongo.Session.Cache;

namespace FluentMongo.Session
{
    public class MongoContext : IMongoContext
    {
        private readonly IChangeTracker _changeTracker;

        public MongoDB.Driver.MongoDatabase Database { get; private set; }

        public MongoContext(MongoDatabase database)
        {
            Database = database;
            _changeTracker = new StandardChangeTracker();
        }

        public IQueryable<T> Find<T>(string collectionName)
        {
            var classMap = BsonClassMap.LookupClassMap(typeof(T));
            var collection = Database.GetCollection<T>(collectionName);

            return new CacheableQuery<T>(
                new MongoQuery<T>(new CacheableQueryProvider(collection, classMap, _changeTracker)), 
                classMap, 
                _changeTracker);
        }

        public void Remove<T>(string collectionName, T entity)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(string collectionName, T entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }
}
