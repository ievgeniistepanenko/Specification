using Specification;
using System;
using System.Linq.Expressions;

namespace Specifications
{
    internal sealed class AllSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}