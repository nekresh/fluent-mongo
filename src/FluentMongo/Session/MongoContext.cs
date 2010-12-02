using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMongo.Linq;
using MongoDB.Driver;
using MongoDB.Bson.DefaultSerializer;
using FluentMongo.Session.Conventions;
using FluentMongo.Session.Tracking;

namespace FluentMongo.Session
{
    public class MongoContext : IMongoContext
    {
        private IChangeTracker _changeTracker;
        private MongoDatabase _database;
        private bool _disposed;

        public MongoDatabase Database
        {
            get 
            {
                EnsureNotDisposed();
                return _database;
            }
        }

        public MongoContext(MongoDatabase database)
        {
            _database = database;
            _changeTracker = new StandardChangeTracker();
        }

        ~MongoContext()
        {
            Dispose(false);
        }

        public IQueryable<T> Find<T>(string collectionName)
        {
            EnsureNotDisposed();
            var collection = Database.GetCollection<T>(collectionName);

            return new CacheableQuery<T>(
                new MongoQuery<T>(new CacheableQueryProvider(collection, _changeTracker)), 
                _changeTracker);
        }

        public void Remove<T>(string collectionName, T entity)
        {
            EnsureNotDisposed();
            throw new NotImplementedException();
        }

        public void Save<T>(string collectionName, T entity)
        {
            EnsureNotDisposed();
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            _changeTracker.Dispose();
            _changeTracker = null;
            _database = null;
            _disposed = true;
        }

        protected void EnsureNotDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);
        }
    }
}
