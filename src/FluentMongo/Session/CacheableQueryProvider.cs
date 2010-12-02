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
using FluentMongo.Session.Tracking;

namespace FluentMongo.Session
{
    internal class CacheableQueryProvider : MongoQueryProvider
    {
        private readonly IChangeTracker _changeTracker;

        public CacheableQueryProvider(MongoCollection collection, IChangeTracker changeTracker)
            : base(collection)
        {
            _changeTracker = changeTracker;
        }

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public override IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new CacheableQuery<TElement>(base.CreateQuery<TElement>(expression), _changeTracker);
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
                return (IQueryable)Activator.CreateInstance(typeof(CacheableQuery<>).MakeGenericType(elementType), new object[] { base.CreateQuery(expression), _changeTracker });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
