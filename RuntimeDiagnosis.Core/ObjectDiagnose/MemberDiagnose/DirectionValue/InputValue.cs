namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

public sealed class InputValue<TOwnerType, TMemberValueType> : 
    DirectionValue<TOwnerType, TMemberValueType?>, IInputValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<TMemberValueType?> _setCurrentValue;
    
    public InputValue(IMemberDiagnose<TOwnerType, TMemberValueType?> memberDiagnose,
        Action<TMemberValueType?> setCurrentValue) : base(memberDiagnose) =>
        _setCurrentValue = setCurrentValue;

    protected override void SetCurrentValue(TMemberValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        _setCurrentValue(value);
        base.SetCurrentValue(value, setAgainEvenIfNotChanged);
    }
}