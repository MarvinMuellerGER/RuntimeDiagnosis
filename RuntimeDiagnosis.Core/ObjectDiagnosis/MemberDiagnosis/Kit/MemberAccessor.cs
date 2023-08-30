using System.Linq.Expressions;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

public sealed class MemberAccessor<T> : IMemberAccessorInternal<T>
{
    private Func<T?> _getter = null!;
    private Action<T?> _setter = null!;

    object? IMemberAccessor.Value
    {
        get => Value;
        set
        {
            Value = value switch
            {
                T valueCasted => valueCasted,
                null => default,
                _ => Value
            };
        }
    }

    public T? Value
    {
        get => _getter();
        set => _setter(value);
    }

    void IMemberAccessorInternal<T>.Initialize(Expression<Func<T?>> expression)
    {
        if (expression.Body is not MemberExpression memberExpression)
            throw new ArgumentException($"{nameof(expression)} must return a field or property");

        var parameterExpression = Expression.Parameter(typeof(T?));
        
        _getter = expression.Compile();
        _setter = Expression.Lambda<Action<T?>>(Expression.Assign(memberExpression, parameterExpression), 
            parameterExpression).Compile();
    }
}