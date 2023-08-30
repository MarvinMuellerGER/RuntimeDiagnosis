namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public class TrackableValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType> : 
    TrackableValue<TOwnerType, TMemberValueType?, TValueType?>, 
    ITrackableValueAlwaysEditableInternal<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    private bool _editingCurrentlyAllowed = true;
    
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

    protected void Initialize(
        IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis, string name, bool editingCurrentlyAllowed)
    {
        base.Initialize(directionValueDiagnosis, name);
        _editingCurrentlyAllowed = editingCurrentlyAllowed;
    }

    private new void SetValue(TValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        if (!EditingCurrentlyAllowed)
            throw new AccessViolationException();
        base.SetValue(value, setAgainEvenIfNotChanged);
    }
}