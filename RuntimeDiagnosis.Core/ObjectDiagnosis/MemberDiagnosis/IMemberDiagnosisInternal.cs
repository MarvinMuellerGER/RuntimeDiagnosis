using System.Linq.Expressions;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

public interface IMemberDiagnosisInternal<TOwnerType, TMemberValueType> : IMemberDiagnosis<TOwnerType, TMemberValueType>
    where TOwnerType : IDiagnosableObject
{
    internal void Initialize(in IObjectDiagnosis<TOwnerType> objectDiagnosis, in string memberName,
        Expression<Func<TMemberValueType?>> memberExpression,
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions, 
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions);
}