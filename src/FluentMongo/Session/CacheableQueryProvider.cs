using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MongoDB.Bson.DefaultSerializer;
using FluentMongo.Linq;
using System.Linq.Expressions;
using FluentMongo.Linq.Util;
using System.Reflection;
using MongoDB.Driver;

namespace FluentMongo.Session
{
    internal class CacheableQueryProvider : MongoQueryProvider
    {
        private readonly BsonClassMap _classMap;
        private readonly EntityCache _entityCache;

        public CacheableQueryProvider(MongoCollection collection, BsonClassMap classMap, EntityCache entityCache)
            : base(collection)
        {
            _classMap = classMap;
            _entityCache = entityCache;
        }

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public override IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new CacheableQuery<TElement>(base.CreateQuery<TElement>(expression), _classMap, _entityCache);
        }

        /// <summary>
        /// Constructs an <see cref="T:System.Linq.IQueryable"/> object that can evaluate the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>
        /// An <see cref="T:System.Linq.IQueryable"/> that can evaluate the query represented by the specified expression tree.
        /// </returns>
        public override IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeHelper.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(CacheableQuery<>).MakeGenericType(elementType), new object[] { base.CreateQuery(expression), _classMap, _entityCache });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
