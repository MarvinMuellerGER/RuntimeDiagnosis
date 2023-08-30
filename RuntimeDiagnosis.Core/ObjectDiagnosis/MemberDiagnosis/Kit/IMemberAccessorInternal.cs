using System.Linq.Expressions;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

/// <inheritdoc />
/// <remarks>This interface gives access to <see cref="Initialize"/> Method</remarks>
public interface IMemberAccessorInternal<TMember> : IMemberAccessor<TMember>
{
    /// <summary>
    /// Initializes get and set accessors for the member described by the supplied lambda expression.
    /// </summary>
    /// <param name="expression">A lambda expression that returns the member to be accessed.</param>
    /// <typeparam name="TMember">Datatype of the accessed member</typeparam>
    /// <exception cref="ArgumentException">Thrown if the expression does not return a field or property.</exception>
    void Initialize(Expression<Func<TMember?>> expression);
}