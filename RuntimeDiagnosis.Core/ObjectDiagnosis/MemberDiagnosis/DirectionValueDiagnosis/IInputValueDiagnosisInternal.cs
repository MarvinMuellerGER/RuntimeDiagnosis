using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

// TODO: Write Summaries
public interface IInputValueDiagnosisInternal<TOwnerType, TMemberValueType> :
    IInputValueDiagnosis<TOwnerType, TMemberValueType>
    where TOwnerType : IDiagnosableObject
{
    internal void Initialize(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<DirectionValueDefinition> callerDefinitions);
}