using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.DefaultSerializer;
using FluentMongo.Session.Conventions;

namespace FluentMongo.Session
{
    public static class IMongoContextExtensions
    {
        public static IQueryable<T> Find<T>(this IMongoContext context)
        {
            var collectionName = GetCollectionName(typeof(T));
            return context.Find<T>(collectionName);
        }

        private static string GetCollectionName(Type type)
        {
            var classMap = BsonClassMap.LookupClassMap(type);
            return GetCollectionName(classMap);
        }

        private static string GetCollectionName(BsonClassMap classMap)
        {
            if (classMap.BaseClassMap != null)
                return GetCollectionName(classMap.BaseClassMap);

            ICollectionNameConvention convention;
            if (!classMap.TryGetExtension<ICollectionNameConvention>(out convention))
                convention = new TypeNameCollectionNameConvention();

            return convention.GetCollectionName(classMap.ClassType);
        }
    }
}