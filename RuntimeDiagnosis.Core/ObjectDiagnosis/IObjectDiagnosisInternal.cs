using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

// TODO: Write Summaries
public interface IObjectDiagnosisInternal : IObjectDiagnosis
{
    IMemberDiagnosis CreateMemberDiagnosis<TMemberValueType>(
        in string memberName,
        Expression<Func<TMemberValueType?>> memberExpression,
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions,
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions);

    public TMemberValueType? GetMemberValue<TMemberValueType>(
        in Expression<Func<TMemberValueType?>> memberExpression,
        [CallerMemberName] string memberName = "");

    public void SetMemberValue<TMemberValueType>(in Expression<Func<TMemberValueType?>> memberExpression,
        in TMemberValueType? value, [CallerMemberName] string memberName = "");
}

public interface IObjectDiagnosisInternal<TOwnerType> : IObjectDiagnosis<TOwnerType>, IObjectDiagnosisInternal
    where TOwnerType : IDiagnosableObject
{
    void Initialize(TOwnerType owner);
}