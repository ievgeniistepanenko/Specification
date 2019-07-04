using Specification.Interfaces;
using Specifications;
using System;
using System.Linq.Expressions;

namespace Specification
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public static bool operator false(Specification<T> spec) => false;

        public static bool operator true(Specification<T> spec) => false;

        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec) => spec.ToExpression();

        public static implicit operator Func<T, bool>(Specification<T> spec) => spec.IsSatisfiedBy;

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
}