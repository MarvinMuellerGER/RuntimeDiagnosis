using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

public sealed class InputValue<TOwnerType, TMemberValueType> : 
    DirectionValue<TOwnerType, TMemberValueType?>, IInputValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<TMemberValueType?> _setCurrentValue;
    
    TMemberValueType? IInputValue<TMemberValueType?>.Value
    {
        set
        {
            InvokeJustCalled();
            OriginalValue.Value = value;
        }
    }
    
    public InputValue(IMemberDiagnose<TOwnerType, TMemberValueType?> memberDiagnose, 
        IEnumerable<DirectionValueDefinition> callerDefinitions, Action<TMemberValueType?> setCurrentValue) : 
        base(memberDiagnose, callerDefinitions) =>
        _setCurrentValue = setCurrentValue;

    protected override void SetCurrentValue(TMemberValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        _setCurrentValue(value);
        base.SetCurrentValue(value, setAgainEvenIfNotChanged);
    }
}