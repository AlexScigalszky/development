using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace TatuelAPI.Utilities
{
    public enum OperationEnum
    {
        AND = 1,
        OR = 2,
    }

    /// <summary>
    /// https://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
    /// </summary>
    public class LambdaUtilsMethods
    {

        public class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }

        public static Expression<Func<T, bool>> CombineAnd<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return Combine(expr1, expr2, OperationEnum.AND);
        }

        public static Expression<Func<T, bool>> CombineOr<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return Combine(expr1, expr2, OperationEnum.OR);
        }

        public static Expression<Func<T, bool>> Combine<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2, OperationEnum op)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);
            if (op == OperationEnum.AND)
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
            else if (op == OperationEnum.OR)
                return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
            else
                throw new NotImplementedException();
        }

    }
}