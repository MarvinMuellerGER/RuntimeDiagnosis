namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public class TrackableValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType> : 
    TrackableValue<TOwnerType, TMemberValueType?, TValueType?>, 
    ITrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>
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
    
    public TrackableValueAlwaysEditable(IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis, string name) : 
        this(directionValueDiagnosis, name, true)
    { }

    protected TrackableValueAlwaysEditable(
        IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis, string name, bool editingCurrentlyAllowed) : 
        base(directionValueDiagnosis, name) =>
        _editingCurrentlyAllowed = editingCurrentlyAllowed;

    public new void SetValue(TValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        if (!EditingCurrentlyAllowed)
            throw new AccessViolationException();
        base.SetValue(value, setAgainEvenIfNotChanged);
    }
}