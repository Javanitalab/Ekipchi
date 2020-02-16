using System;
using System.Linq.Expressions;

namespace Hastnama.Ekipchi.DataAccess.Extensions
{
    public static class ExpressionExtensions
    {
        public static string AsPath(this LambdaExpression expression)
        {
            if (expression == null)
                return (string) null;
            string path;
            ExpressionExtensions.TryParsePath(expression.Body, out path);
            return path;
        }

        private static bool TryParsePath(Expression expression, out string path)
        {
            path = (string) null;
            Expression expression1 = ExpressionExtensions.RemoveConvert(expression);
            MemberExpression memberExpression = expression1 as MemberExpression;
            MethodCallExpression methodCallExpression = expression1 as MethodCallExpression;
            if (memberExpression != null)
            {
                string name = memberExpression.Member.Name;
                string path1;
                if (!ExpressionExtensions.TryParsePath(memberExpression.Expression, out path1))
                    return false;
                path = path1 == null ? name : path1 + "." + name;
            }
            else if (methodCallExpression != null)
            {
                if (methodCallExpression.Method.Name == "Select" && methodCallExpression.Arguments.Count == 2)
                {
                    string path1;
                    string path2;
                    if (!ExpressionExtensions.TryParsePath(methodCallExpression.Arguments[0], out path1) ||
                        (path1 == null || !(methodCallExpression.Arguments[1] is LambdaExpression lambdaExpression)) ||
                        (!ExpressionExtensions.TryParsePath(lambdaExpression.Body, out path2) || path2 == null))
                        return false;
                    path = path1 + "." + path2;
                    return true;
                }

                if (methodCallExpression.Method.Name == "Where")
                    throw new NotSupportedException("Filtering an Include expression is not supported");
                if (methodCallExpression.Method.Name == "OrderBy" ||
                    methodCallExpression.Method.Name == "OrderByDescending")
                    throw new NotSupportedException("Ordering an Include expression is not supported");
                return false;
            }

            return true;
        }

        private static Expression RemoveConvert(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert ||
                   expression.NodeType == ExpressionType.ConvertChecked)
                expression = ((UnaryExpression) expression).Operand;
            return expression;
        }
    }
}