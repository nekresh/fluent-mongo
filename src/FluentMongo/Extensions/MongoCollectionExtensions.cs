using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MongoDB.Driver.Builders;
using FluentMongo.Linq;

namespace MongoDB.Driver
{
    public static class MongoCollectionExtensions
    {
        public static SafeModeResult Update<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate, IMongoUpdate update)
        {
            return collection.Update(predicate, update, UpdateFlags.None, collection.Settings.SafeMode);
        }

        public static SafeModeResult Update<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate, IMongoUpdate update, SafeMode safeMode)
        {
            return collection.Update(predicate, update, UpdateFlags.None, safeMode);
        }

        public static SafeModeResult Update<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate, IMongoUpdate update, UpdateFlags flags)
        {
            return collection.Update(predicate, update, flags, collection.Settings.SafeMode);
        }

        public static SafeModeResult Update<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate, IMongoUpdate update, UpdateFlags flags, SafeMode safeMode)
        {
            var query = ((IMongoQueryable)collection.AsQueryable().Where(predicate)).GetQueryObject();

            return collection.Update(new QueryComplete(query.Query), update, flags, safeMode);
        }

        public static SafeModeResult Remove<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate)
        {
            return collection.Remove(predicate, RemoveFlags.None, collection.Settings.SafeMode);
        }

        public static SafeModeResult Remove<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate, SafeMode safeMode)
        {
            return collection.Remove(predicate, RemoveFlags.None, safeMode);
        }

        public static SafeModeResult Remove<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate, RemoveFlags flags)
        {
            return collection.Remove(predicate, flags, collection.Settings.SafeMode);
        }

        public static SafeModeResult Remove<TDefaultDocument>(this MongoCollection<TDefaultDocument> collection, Expression<Func<TDefaultDocument, bool>> predicate, RemoveFlags flags, SafeMode safeMode)
        {
            var query = ((IMongoQueryable)collection.AsQueryable().Where(predicate)).GetQueryObject();

            return collection.Remove(new QueryComplete(query.Query), flags, safeMode);
        }
    }
}
