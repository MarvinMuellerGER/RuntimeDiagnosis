using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

public sealed class InputValueDiagnosis<TOwnerType, TMemberValueType> : 
    DirectionValueDiagnosis<TOwnerType, TMemberValueType?>, IInputValueDiagnosis<TOwnerType, TMemberValueType?>
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
    
    public InputValueDiagnosis(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<DirectionValueDefinition> callerDefinitions) : 
        base(memberDiagnosis, callerDefinitions)
    { }

    protected override void SetCurrentValue(TMemberValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        MemberDiagnosis.MemberValue = value;
        base.SetCurrentValue(value, setAgainEvenIfNotChanged);
    }
}