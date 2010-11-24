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
        public MongoDB.Driver.MongoDatabase Database { get; private set; }

        public MongoContext(MongoDatabase database)
        {
            Database = database;
        }

        public IQueryable<T> Find<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName).AsQueryable();
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
            throw new NotImplementedException();
        }

    }
}
