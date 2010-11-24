using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver;

namespace FluentMongo.Session
{
    public interface IMongoContext : IDisposable
    {
        MongoDatabase Database { get; }

        IQueryable<T> Find<T>(string collectionName);

        void Remove<T>(string collectionName, T entity);

        void Save<T>(string collectionName, T entity);
    }
}