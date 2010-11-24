using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMongo.Session.Conventions
{
    public class TypeNameCollectionNameConvention : ICollectionNameConvention
    {
        public string GetCollectionName(Type type)
        {
            return type.Name;
        }
    }
}