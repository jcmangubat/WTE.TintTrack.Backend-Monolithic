using System.Linq.Expressions;
using System.Reflection;

namespace WTE.TintTrack.Common.Helpers;

public class PredicateConverter<TEntity, TEntityDto> : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    public PredicateConverter()
    {
        _parameter = Expression.Parameter(typeof(TEntity), "entity");
    }

    public Expression<Func<TEntity, bool>> Convert(Expression<Func<TEntityDto, bool>> dtoPredicate)
    {
        var body = Visit(dtoPredicate.Body);
        return Expression.Lambda<Func<TEntity, bool>>(body, _parameter);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        // Replace parameter with the new one for TEntity
        return _parameter;
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        if (node.Member.DeclaringType == typeof(TEntityDto))
        {
            // Resolve property chain for navigation properties
            var entityProperty = ResolveEntityProperty(node);
            if (entityProperty != null)
            {
                return Expression.Property(_parameter, entityProperty);
            }
        }
        return base.VisitMember(node);
    }

    private PropertyInfo ResolveEntityProperty(MemberExpression node)
    {
        var entityType = typeof(TEntity);
        Stack<string> propertyChain = new Stack<string>();

        // Traverse up the expression tree to build property chain
        while (node != null)
        {
            propertyChain.Push(node.Member.Name);
            node = node.Expression as MemberExpression;
        }

        // Resolve properties in the chain
        PropertyInfo? propertyInfo = null;
        foreach (var propertyName in propertyChain)
        {
            propertyInfo = entityType.GetProperty(propertyName);
            if (propertyInfo == null) break;
            entityType = propertyInfo.PropertyType;
        }

        return propertyInfo;
    }
}
