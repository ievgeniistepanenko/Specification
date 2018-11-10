using System;
using System.Linq.Expressions;

namespace Specification
{
    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            if (_left == All || _right == All) return All.ToExpression();
            if (_left == None) return rightExpression;
            if (_right == None) return leftExpression;

            var parameter = Expression.Parameter(typeof(T));
            var leftVisitor = new ReplaceExpressionVisitor(leftExpression.Parameters[0], parameter);
            var left = leftVisitor.Visit(leftExpression.Body);
            var rightVisitor = new ReplaceExpressionVisitor(rightExpression.Parameters[0], parameter);
            var right = rightVisitor.Visit(rightExpression.Body);
            var orExpression = Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(left, right), parameter);

            return orExpression;
        }
    }
}