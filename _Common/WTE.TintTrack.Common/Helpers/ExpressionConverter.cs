using System.Linq.Expressions;

namespace WTE.TintTrack.Common.Helpers;

public static class ExpressionConverter
{
    public static Expression<Func<TEntity, TProperty>> ConvertExpression<TEntity, TEntityDto, TProperty>(
        Expression<Func<TEntityDto, object>> include)
    {
        // Ensure the input expression is a member access or a chain of member accesses
        if (include.Body is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert)
        {
            // Unwrap the UnaryExpression (caused by object return type)
            var memberExpression = unaryExpression.Operand as MemberExpression 
                ?? throw new InvalidOperationException("Expected a MemberExpression.");

            return RebuildExpression<TEntity, TProperty>(memberExpression);
        }
        else if (include.Body is MemberExpression memberExpression)
        {
            return RebuildExpression<TEntity, TProperty>(memberExpression);
        }
        else
        {
            throw new InvalidOperationException("Unsupported expression type.");
        }
    }

    private static Expression<Func<TEntity, TProperty>> RebuildExpression<TEntity, TProperty>(MemberExpression memberExpression)
    {
        // Rebuild the expression tree for TEntity
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var newMemberAccess = RebuildMemberAccess(memberExpression, parameter);

        // Return the new lambda expression
        return Expression.Lambda<Func<TEntity, TProperty>>(newMemberAccess, parameter);
    }

    private static Expression RebuildMemberAccess(MemberExpression memberExpression, ParameterExpression parameter)
    {
        // Base case: if the expression is a direct property, just rebuild it
        if (memberExpression.Expression is ParameterExpression)
        {
            return Expression.PropertyOrField(parameter, memberExpression.Member.Name);
        }

        // Recursively rebuild the parent expression
        var parentExpression = RebuildMemberAccess((MemberExpression)memberExpression.Expression, parameter);
        return Expression.PropertyOrField(parentExpression, memberExpression.Member.Name);
    }
}