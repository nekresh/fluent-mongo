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

namespace FluentMongo.Session
{
    internal class CacheableQueryProvider : IMongoQueryProvider
    {
        private readonly BsonClassMap _classMap;
        private readonly EntityCache _entityCache;
        private readonly IMongoQueryProvider _wrappedQueryProvider;

        public MongoDB.Driver.MongoCollection Collection
        {
            get { return _wrappedQueryProvider.Collection; }
        }

        public CacheableQueryProvider(BsonClassMap classMap, EntityCache entityCache, IMongoQueryProvider wrappedQueryProvider)
        {
            _classMap = classMap;
            _entityCache = entityCache;
            _wrappedQueryProvider = wrappedQueryProvider;
        }

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MongoQuery<TElement>(this, expression);
        }

        /// <summary>
        /// Constructs an <see cref="T:System.Linq.IQueryable"/> object that can evaluate the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>
        /// An <see cref="T:System.Linq.IQueryable"/> that can evaluate the query represented by the specified expression tree.
        /// </returns>
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeHelper.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(MongoQuery<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Executes the specified expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public TResult Execute<TResult>(Expression expression)
        {
            //TODO: add caching logic here...
            return _wrappedQueryProvider.Execute<TResult>(expression);
        }

        /// <summary>
        /// Executes the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>
        /// The value that results from executing the specified query.
        /// </returns>
        public object Execute(Expression expression)
        {
            //TODO: add caching logic here...
            return _wrappedQueryProvider.Execute(expression);
        }

        /// <summary>
        /// Executes the query object.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns></returns>
        public object ExecuteQueryObject(MongoQueryObject queryObject)
        {
            return _wrappedQueryProvider.ExecuteQueryObject(queryObject);
        }

        /// <summary>
        /// Gets the query object.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public MongoQueryObject GetQueryObject(Expression expression)
        {
            return _wrappedQueryProvider.GetQueryObject(expression);
        }
    }
}
