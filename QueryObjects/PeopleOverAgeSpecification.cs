using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryObjects
{
    public class PeopleOverAgeSpecification : Specification<Person>
    {
        private readonly int age;

        public PeopleOverAgeSpecification(int age)
        {
            this.age = age;
        }

        public override IQueryable<Person> Reduce(IQueryable<Person> collection)
        {
            return collection.Where(person => person.Age > age);
        }

        public override IQueryable<Person> Sort(IQueryable<Person> collection)
        {
            return collection.OrderBy(person => person.Name);
        }
    }
}
