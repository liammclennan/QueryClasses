using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using NSubstitute;
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
            var allPeopleOfDrivingAge = session.Spec().Query(canDriveSpecification);
        }

        [Test]
        public void MockASpecification()
        {
            var canDriveSpecification = new PeopleOverAgeSpecification(16);
            var queryer = Substitute.For<ISpecificationQueryer>();
            queryer.Query(Arg.Any<PeopleOverAgeSpecification>()).Returns(new List<Person> { new Person() });
            SpecificationExtensions.SpecificationQueryerFactory = s => queryer;
            var allPeopleOfDrivingAge = session.Spec().Query(canDriveSpecification);
            Assert.AreEqual(1, allPeopleOfDrivingAge.Count());
        }

        private ISessionFactory sessionFactory;
        private ISession session;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            //sessionFactory = new SessionFactoryFactory().Build();
        }

        [SetUp]
        public void BeforeTest()
        {
            //session = sessionFactory.OpenSession();
        }

        [TearDown]
        public void AfterTest()
        {
            //session.Dispose();
        }
    }

    public static class SpecificationExtensions
    {
        public static ISpecificationQueryer Spec(this ISession target)
        {
            return SpecificationQueryerFactory(target);
        }

        static SpecificationExtensions()
        {
            SpecificationQueryerFactory = st => new SpecificationQueryer(st);
        }

        internal static Func<ISession, ISpecificationQueryer> SpecificationQueryerFactory { get; set; }
    }

    public interface ISpecificationQueryer
    {
            IEnumerable<T> Query<T>(Specification<T> specification);
    }

    public class SpecificationQueryer : ISpecificationQueryer
    {
        ISession session;

        public SpecificationQueryer(ISession session)
        {
            this.session = session;
        }

        public SpecificationQueryer()
        {
        }

        public IEnumerable<T> Query<T>(Specification<T> specification)
        {
            return specification.Fetch(
                    specification.Sort(
                        specification.Reduce(session.Query<T>())
                    )
                );
        }
    }
}
