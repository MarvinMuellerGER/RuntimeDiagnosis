using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue;

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
    
    public InputValue(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<DirectionValueDefinition> callerDefinitions, Action<TMemberValueType?> setCurrentValue) : 
        base(memberDiagnosis, callerDefinitions) =>
        _setCurrentValue = setCurrentValue;

    protected override void SetCurrentValue(TMemberValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        _setCurrentValue(value);
        base.SetCurrentValue(value, setAgainEvenIfNotChanged);
    }
}