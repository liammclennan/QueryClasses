using System.Linq;

namespace QueryObjects
{
    public abstract class Specification<T>
    {
        public virtual IQueryable<T> Reduce(IQueryable<T> collection)
        {
            return collection;
        }

        public virtual IQueryable<T> Sort(IQueryable<T> collection)
        {
            return collection;
        }

        public virtual IQueryable<T> Fetch(IQueryable<T> collection)
        {
            return collection;
        }
    }
}