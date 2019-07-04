using Specification;
using System;
using System.Linq.Expressions;

namespace Specifications
{
    internal sealed class NoneSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => false;
        }
    }
}