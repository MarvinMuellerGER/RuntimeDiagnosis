namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public sealed class TrackableValueEditable<TOwnerType, TMemberValueType, TValueType> : 
    TrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>, 
    ITrackableValueEditableInternal<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    public new bool EditingCurrentlyAllowed
    {
        get => base.EditingCurrentlyAllowed;
        private set => base.EditingCurrentlyAllowed = value;
    }

    bool ITrackableValueEditableInternal.EditingCurrentlyAllowed
    {
        set => EditingCurrentlyAllowed = value;
    }

    void ITrackableValueEditableInternal<TOwnerType, TMemberValueType?, TValueType?>.Initialize(
        IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis,
        string name, bool editingCurrentlyAllowed) =>
        base.Initialize(directionValueDiagnosis, name, editingCurrentlyAllowed);
}