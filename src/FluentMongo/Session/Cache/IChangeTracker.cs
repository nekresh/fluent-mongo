using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.DefaultSerializer;

namespace FluentMongo.Session.Cache
{
    internal interface IChangeTracker
    {
        ITrackedObject GetTrackedObject(object obj);

        bool IsTracked(object obj);

        void StopTracking(object obj);

        ITrackedObject Track(BsonClassMap classMap, object obj);
    }
}