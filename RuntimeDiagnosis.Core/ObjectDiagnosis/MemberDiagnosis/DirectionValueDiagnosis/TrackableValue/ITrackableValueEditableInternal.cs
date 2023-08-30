namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public interface ITrackableValueEditableInternal : ITrackableValueEditable
{
    internal new bool EditingCurrentlyAllowed { set; }
}

public interface ITrackableValueEditableInternal<TValueType> :
    ITrackableValueEditableInternal, ITrackableValueEditable<TValueType>
{ }

public interface ITrackableValueEditableInternal<TMemberValueType, TValueType> : 
    ITrackableValueEditableInternal<TValueType?>, ITrackableValueEditable<TMemberValueType, TValueType>
{ }

public interface ITrackableValueEditableInternal<TOwnerType, TMemberValueType, TValueType> :
    ITrackableValueEditableInternal<TMemberValueType?, TValueType?>,
    ITrackableValueEditable<TOwnerType, TMemberValueType, TValueType>
    where TOwnerType : IDiagnosableObject
{
    internal void Initialize(IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis,
        string name, bool editingCurrentlyAllowed = false);
}