using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NUnit.Framework;

namespace QueryObjects
{
    [TestFixture]
    public class Example
    {
        [Test]
        public void QueryBySpecification()
        {
            var canDriveSpecification = new PeopleOverAgeSpecification(16);
            var allPeopleOfDrivingAge = session.QueryBySpecification(canDriveSpecification);
        }

        private ISessionFactory sessionFactory;
        private ISession session;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            sessionFactory = new SessionFactoryFactory().Build();
        }

        [SetUp]
        public void BeforeTest()
        {
            session = sessionFactory.OpenSession();
        }

        [TearDown]
        public void AfterTest()
        {
            session.Dispose();
        }
    }
}
