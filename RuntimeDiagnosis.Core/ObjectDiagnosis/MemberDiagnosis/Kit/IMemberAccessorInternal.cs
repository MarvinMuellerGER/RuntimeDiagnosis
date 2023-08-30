using System.Linq.Expressions;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

public interface IMemberAccessorInternal<T> : IMemberAccessor<T>
{
    internal void Initialize(Expression<Func<T?>> expression);
}