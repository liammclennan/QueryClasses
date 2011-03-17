using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Linq;

namespace QueryObjects
{
    public static class SessionExtensions
    {
        public static IEnumerable<T> QueryBySpecification<T>(this ISession session, Specification<T> specification)
        {
            return specification.Fetch(
                specification.Sort(
                    specification.Reduce(session.Query<T>())
                )
            );
        }
    }
}
