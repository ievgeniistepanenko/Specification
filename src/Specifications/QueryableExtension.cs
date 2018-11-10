using System.Linq;

namespace Specification
{
    public static class QueryableExtension
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> source, Specification<T> spec)
        {
            return source.Where(spec.ToExpression());
        }
    }
}