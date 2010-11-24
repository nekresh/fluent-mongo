using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FluentMongo.Linq
{
    internal interface IMongoQueryProvider : IQueryProvider
    {
        MongoCollection Collection { get; }

        object ExecuteQueryObject(MongoQueryObject queryObject);

        MongoQueryObject GetQueryObject(Expression expression);
    }
}
