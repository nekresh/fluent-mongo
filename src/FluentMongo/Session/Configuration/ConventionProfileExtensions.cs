using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson.DefaultSerializer.Conventions;
using FluentMongo.Session.Conventions;

namespace FluentMongo.Session.Configuration
{
    public static class ConventionProfileExtensions
    {
        public static ConventionProfile SetCollectionNameConvention(this ConventionProfile profile, ICollectionNameConvention convention)
        {
            profile.SetExtension<ICollectionNameConvention>(convention);
            return profile;
        }
    }
}