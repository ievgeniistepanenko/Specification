using Specification.Interfaces;
using System;
using System.Linq.Expressions;

namespace Specification
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public static Specification<T> operator &(Specification<T> leftSpec, Specification<T> rightSpec)
        {
            return new AndSpecification<T>(leftSpec, rightSpec);
        }

        public static Specification<T> operator |(Specification<T> leftSpec, Specification<T> rightSpec)
        {
            return new OrSpecification<T>(leftSpec, rightSpec);
        }

        public static Specification<T> operator !(Specification<T> spec)
        {
            return new NotSpecification<T>(spec);
        }

        public static readonly Specification<T> All = new AllSpecification<T>();
        public static readonly Specification<T> None = new NoneSpecification<T>();
    }

    internal sealed class AllSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }

    internal sealed class NoneSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => false;
        }
    }
}