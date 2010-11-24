using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMongo.Session
{
    public interface IMongoContextFactory
    {
        IMongoContext CreateMongoContext();
    }
}