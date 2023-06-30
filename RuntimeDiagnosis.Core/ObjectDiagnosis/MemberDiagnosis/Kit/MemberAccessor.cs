using System.Linq.Expressions;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

public class MemberAccessor<T>
{
    private readonly Func<T?> _getter;
    private readonly Action<T?> _setter;

    public T? Value
    {
        get => _getter();
        set => _setter(value);
    }

    public MemberAccessor(Expression<Func<T?>> expression)
    {
        if (expression.Body is not MemberExpression memberExpression)
            throw new ArgumentException($"{nameof(expression)} must return a field or property");

        var parameterExpression = Expression.Parameter(typeof(T?));
        
        _getter = expression.Compile();
        _setter = Expression.Lambda<Action<T?>>(Expression.Assign(memberExpression, parameterExpression), 
            parameterExpression).Compile();
    }
}