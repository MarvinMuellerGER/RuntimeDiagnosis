namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.SingleValue;

public class SingleValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType> : 
    SingleValue<TOwnerType, TMemberValueType?, TValueType?>, 
    ISingleValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    private bool _editingCurrentlyAllowed;
    
    public new TValueType? Value
    {
        get => base.Value;
        set => SetValue(value);
    }

    protected bool EditingCurrentlyAllowed
    {
        get => _editingCurrentlyAllowed;
        set => SetField(ref _editingCurrentlyAllowed, value);
    }
    
    public SingleValueAlwaysEditable(IDirectionValue<TOwnerType, TMemberValueType?> directionValue, string name) : 
        this(directionValue, name, true)
    { }

    protected SingleValueAlwaysEditable(
        IDirectionValue<TOwnerType, TMemberValueType?> directionValue, string name, bool editingCurrentlyAllowed) : 
        base(directionValue, name) =>
        _editingCurrentlyAllowed = editingCurrentlyAllowed;

    public new void SetValue(TValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        if (!EditingCurrentlyAllowed)
            throw new AccessViolationException();
        base.SetValue(value, setAgainEvenIfNotChanged);
    }
}