using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

using NUnit.Framework;

namespace FluentMongo.Session
{
    [TestFixture]
    public class MongoContextTests : TestBase
    {
        public override void SetupFixture()
        {
            base.SetupFixture();

            var collection = GetCollection<Person>("people");

            collection.Insert(
                new Person
                {
                    FirstName = "Bob",
                    MidName = "Bart",
                    LastName = "McBob",
                    Age = 42,
                    PrimaryAddress = new Address { City = "London", IsInternational = true, AddressType = AddressType.Company },
                    Addresses = new[]
                    {
                        new Address { City = "London", IsInternational = true, AddressType = AddressType.Company },
                        new Address { City = "Tokyo", IsInternational = true, AddressType = AddressType.Private }, 
                        new Address { City = "Seattle", IsInternational = false, AddressType = AddressType.Private },
                        new Address { City = "Paris", IsInternational = true, AddressType = AddressType.Private } 
                    },
                    EmployerIds = new[] { 1, 2 }
                }, SafeMode.True);

            collection.Insert(
                new Person
                {
                    FirstName = "Jane",
                    LastName = "McJane",
                    Age = 35,
                    PrimaryAddress = new Address { City = "Paris", IsInternational = false, AddressType = AddressType.Private },
                    Addresses = new[]
                    {
                        new Address { City = "Paris", AddressType = AddressType.Private }
                    },
                    EmployerIds = new[] { 1 }
                },
                SafeMode.True);

            collection.Insert(
                new Person
                {
                    FirstName = "Joe",
                    LastName = "McJoe",
                    Age = 21,
                    PrimaryAddress = new Address { City = "Chicago", IsInternational = true, AddressType = AddressType.Private },
                    Addresses = new[]
                    {
                        new Address { City = "Chicago", AddressType = AddressType.Private },
                        new Address { City = "London", AddressType = AddressType.Company }
                    },
                    EmployerIds = new[] { 3 }
                },
                SafeMode.True);
        }

        [Test]
        public void Find_should_be_cached()
        {
            using (var context = new MongoContext(_database))
            {
                var people = context.Find<Person>("people").Where(x => x.Age > 21).ToList();
            }
        }
    }
}