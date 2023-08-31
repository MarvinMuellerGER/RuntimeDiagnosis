using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

// TODO: Write Summaries
public interface IOutputValueDiagnosisInternal<TOwnerType, TMemberValueType> :
    IOutputValueDiagnosis<TOwnerType, TMemberValueType>
    where TOwnerType : IDiagnosableObject
{
    internal void Initialize(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<IDirectionValueDefinition> callerDefinitions, Action<EventHandler> attachToInputValueChanged);
}