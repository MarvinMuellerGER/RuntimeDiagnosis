namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public sealed class TrackableValueEditable<TOwnerType, TMemberValueType, TValueType> : 
    TrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>, 
    ITrackableValueEditable<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    public new bool EditingCurrentlyAllowed
    {
        get => base.EditingCurrentlyAllowed;
        internal set => base.EditingCurrentlyAllowed = value;
    }

    public TrackableValueEditable(IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis, 
        string name, bool editingCurrentlyAllowed = false) :
        base(directionValueDiagnosis, name, editingCurrentlyAllowed)
    { }
}