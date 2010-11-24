using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentMongo.Linq;
using MongoDB.Bson.DefaultSerializer;
using FluentMongo.Session.Conventions;

namespace FluentMongo.Session
{
    [TestFixture]
    public class IMongoContextExtensionsTests : TestBase
    {
        private class TestClass { }

        public override void SetupTest()
        {
            base.SetupTest();
            BsonClassMap.UnregisterClassMap(typeof(TestClass));
        }

        [Test]
        public void Should_use_TypeNameCollectionNameConvention_when_none_is_present()
        {
            var context = new MongoContext(_database);

            var queryable = context.Find<TestClass>();
            Assert.AreEqual("TestClass", ((MongoQueryProvider)queryable.Provider).Collection.Name);
        }

        [Test]
        public void Should_use_registered_ICollectionNameConvention_when_one_exists()
        {
            var classMap = BsonClassMap.LookupClassMap(typeof(TestClass));
            classMap.SetExtension<ICollectionNameConvention>(new CamelCasedCollectionNameConvention());
            var context = new MongoContext(_database);

            var queryable = context.Find<TestClass>();
            Assert.AreEqual("testClass", ((MongoQueryProvider)queryable.Provider).Collection.Name);
        }
    }
}
