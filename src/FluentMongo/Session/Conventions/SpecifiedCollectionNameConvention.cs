using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMongo.Session.Conventions
{
    public class SpecifiedCollectionNameConvention : ICollectionNameConvention
    {
        private readonly string _specifiedCollectionName;

        public SpecifiedCollectionNameConvention(string collectionName)
        {
            _specifiedCollectionName = collectionName;
        }

        public string GetCollectionName(Type type)
        {
            return _specifiedCollectionName;
        }
    }
}