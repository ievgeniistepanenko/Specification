using System;
using System.Linq.Expressions;

namespace Specification
{
    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        internal NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();
            var parameter = Expression.Parameter(typeof(T));
            var visitor = new ReplaceExpressionVisitor(expression.Parameters[0], parameter);
            var exp = visitor.Visit(expression.Body);
            var notExpression = Expression.Lambda<Func<T, bool>>(
                Expression.Not(exp), parameter);

            return notExpression;
        }
    }
}