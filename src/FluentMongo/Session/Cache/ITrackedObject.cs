using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.DefaultSerializer;

namespace FluentMongo.Session.Cache
{
    internal interface ITrackedObject
    {
        BsonClassMap ClassMap { get; }

        BsonDocument Original { get; }

        object Current { get; }

        bool IsDeleted { get; }

        bool IsNew { get; }

        bool IsModified { get; }

        bool IsPossiblyModified { get; }
    }
}