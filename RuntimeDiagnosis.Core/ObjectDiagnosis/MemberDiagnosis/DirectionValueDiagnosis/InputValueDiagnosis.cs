using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

public sealed class InputValueDiagnosis<TOwnerType, TMemberValueType> : 
    DirectionValueDiagnosis<TOwnerType, TMemberValueType?>, IInputValueDiagnosis<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<TMemberValueType?> _setCurrentValue;
    
    TMemberValueType? IInputValueDiagnosis<TMemberValueType?>.Value
    {
        set
        {
            InvokeJustCalled();
            OriginalValue.Value = value;
        }
    }
    
    public InputValueDiagnosis(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<DirectionValueDefinition> callerDefinitions, Action<TMemberValueType?> setCurrentValue) : 
        base(memberDiagnosis, callerDefinitions) =>
        _setCurrentValue = setCurrentValue;

    protected override void SetCurrentValue(TMemberValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        _setCurrentValue(value);
        base.SetCurrentValue(value, setAgainEvenIfNotChanged);
    }
}