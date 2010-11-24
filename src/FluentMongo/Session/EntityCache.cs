using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.DefaultSerializer;

namespace FluentMongo.Session
{
    public class EntityCache
    {
        public BsonDocument GetDocumentForUpdate(BsonClassMap classMap, object entity)
        {
            throw new NotImplementedException();
        }

        public bool IsDirty(BsonClassMap classMap, object entity)
        {
            return false;
        }

        public void Store(BsonClassMap classMap, object entity)
        {

        }
    }
}