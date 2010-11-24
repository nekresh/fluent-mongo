using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMongo.Session.Conventions
{
    public class CamelCasedCollectionNameConvention : ICollectionNameConvention
    {
        public string GetCollectionName(Type type)
        {
            return Char.ToLowerInvariant(type.Name[0]) + type.Name.Substring(1);
        }
    }
}