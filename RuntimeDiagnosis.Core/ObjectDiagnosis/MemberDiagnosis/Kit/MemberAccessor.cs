using System.Linq.Expressions;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

/// <inheritdoc />
/// <remarks></remarks>
public sealed class MemberAccessor<TMember> : IMemberAccessorInternal<TMember>
{
    private Func<TMember?> _getter = null!;
    private Action<TMember?> _setter = null!;

    object? IMemberAccessor.Value
    {
        get => Value;
        set
        {
            Value = value switch
            {
                TMember valueCasted => valueCasted,
                null => default,
                _ => Value
            };
        }
    }

    public TMember? Value
    {
        get => _getter();
        set => _setter(value);
    }

    void IMemberAccessorInternal<TMember>.Initialize(Expression<Func<TMember?>> expression)
    {
        if (expression.Body is not MemberExpression memberExpression)
            throw new ArgumentException($"{nameof(expression)} must return a field or property");

        var parameterExpression = Expression.Parameter(typeof(TMember?));
        
        _getter = expression.Compile();
        _setter = Expression.Lambda<Action<TMember?>>(Expression.Assign(memberExpression, parameterExpression), 
            parameterExpression).Compile();
    }
}