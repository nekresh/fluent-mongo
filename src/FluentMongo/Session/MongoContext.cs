using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMongo.Linq;
using MongoDB.Driver;
using MongoDB.Bson.DefaultSerializer;
using FluentMongo.Session.Conventions;

namespace FluentMongo.Session
{
    public class MongoContext : IMongoContext
    {
        private readonly EntityCache _entityCache;

        public MongoDB.Driver.MongoDatabase Database { get; private set; }

        public MongoContext(MongoDatabase database)
        {
            Database = database;
            _entityCache = new EntityCache();
        }

        public IQueryable<T> Find<T>(string collectionName)
        {
            var classMap = BsonClassMap.LookupClassMap(typeof(T));
            var collection = Database.GetCollection<T>(collectionName);

            return new CacheableQuery<T>(
                new MongoQuery<T>(new CacheableQueryProvider(collection, classMap, _entityCache)), 
                classMap, 
                _entityCache);
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
