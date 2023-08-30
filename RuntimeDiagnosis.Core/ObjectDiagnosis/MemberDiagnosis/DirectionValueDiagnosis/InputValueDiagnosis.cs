using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

public sealed class InputValueDiagnosis<TOwnerType, TMemberValueType> : 
    DirectionValueDiagnosis<TOwnerType, TMemberValueType?>, IInputValueDiagnosisInternal<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    TMemberValueType? IInputValueDiagnosis<TMemberValueType?>.Value
    {
        set
        {
            InvokeJustCalled();
            OriginalValue.Value = value;
        }
    }

    public InputValueDiagnosis(
        IObjectDiagnosesManagerInternal objectDiagnosesManager,
        IDirectionValueDiagnosesFinder directionValueDiagnosesFinder,
        ITrackableValueAlwaysEditableInternal<TOwnerType,TMemberValueType?,bool> diagnoseActive,
        ITrackableValueEditableInternal<TOwnerType, TMemberValueType?, TMemberValueType?> diagnoseValue,
        ITrackableValueInternal<TOwnerType,TMemberValueType?,TMemberValueType?> originalValue,
        ITrackableValueInternal<TOwnerType,TMemberValueType?,TMemberValueType?> currentValue) :
        base(objectDiagnosesManager, directionValueDiagnosesFinder, 
            diagnoseActive, diagnoseValue, originalValue, currentValue)
    { }

    public new void Initialize(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis,
        IEnumerable<DirectionValueDefinition> callerDefinitions) =>
        base.Initialize(memberDiagnosis, callerDefinitions);

    protected override void SetCurrentValue(TMemberValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        MemberDiagnosis.MemberValue = value;
        base.SetCurrentValue(value, setAgainEvenIfNotChanged);
    }
}