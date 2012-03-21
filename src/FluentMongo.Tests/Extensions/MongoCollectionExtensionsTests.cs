using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using FluentMongo.Linq;

namespace FluentMongo.Extensions
{
    [TestFixture]
    public class MongoCollectionExtensionsTests : TestBase
    {
        MongoCollection<Person> Collection;

        public override void SetupTest()
        {
            base.SetupTest();

            Collection = GetCollection<Person>("people");

            Collection.Insert(new Person
            {
                Age = 24
            });
            Collection.Insert(new Person
            {
                Age = 42
            });
        }

        public override void TearDownTest()
        {
            base.TearDownTest();

            Collection.RemoveAll();
        }

        [Test]
        public void Update_Simple()
        {
            Collection.Update(p => p.Age < 42, Update.Inc("age", 1));

            Assert.NotNull(Collection.AsQueryable().Where(p => p.Age == 25).SingleOrDefault());
            Assert.NotNull(Collection.AsQueryable().Where(p => p.Age == 42).SingleOrDefault());
        }

        [Test]
        public void Remove_Normal()
        {
            Collection.Remove(p => p.Age < 100);

            Assert.AreEqual(0, Collection.Count());
        }

        [Test]
        public void Remove_Single()
        {
            Collection.Remove(p => p.Age < 100, RemoveFlags.Single);

            Assert.AreEqual(1, Collection.Count());
        }
    }
}
