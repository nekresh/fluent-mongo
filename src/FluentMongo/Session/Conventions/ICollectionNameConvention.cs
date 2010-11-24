using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMongo.Session.Conventions
{
    public interface ICollectionNameConvention
    {
        string GetCollectionName(Type type);
    }
}