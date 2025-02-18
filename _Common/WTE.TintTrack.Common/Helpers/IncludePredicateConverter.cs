using System.Linq.Expressions;

namespace WTE.TintTrack.Common.Helpers;

public class IncludePredicateConverter
{
    public static Expression<Func<TEntity, object>> ConvertIncludeExpression<TEntityDto, TEntity>(
    Expression<Func<TEntityDto, object>> dtoIncludeExpression)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");

        var body = new ExpressionReplacer<TEntityDto, TEntity>().Visit(dtoIncludeExpression.Body);

        return Expression.Lambda<Func<TEntity, object>>(body, parameter);
    }

    internal class ExpressionReplacer<TEntityDto, TEntity> : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TEntityDto))
            {
                // Replace with corresponding member in TEntity
                var newMember = typeof(TEntity).GetMember(node.Member.Name).FirstOrDefault();
                if (newMember != null)
                {
                    return Expression.MakeMemberAccess(node.Expression, newMember);
                }
            }
            return base.VisitMember(node);
        }
    }
}