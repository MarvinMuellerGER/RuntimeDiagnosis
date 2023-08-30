namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

// TODO: Write Summaries
public interface ITrackableValueEditable : ITrackableValueAlwaysEditable
{
    bool EditingCurrentlyAllowed { get; }
}

public interface ITrackableValueEditable<TValueType> : ITrackableValueEditable, ITrackableValueAlwaysEditable<TValueType?>
{ }

public interface ITrackableValueEditable<TMemberValueType, TValueType> : 
    ITrackableValueEditable<TValueType?>, ITrackableValueAlwaysEditable<TMemberValueType?, TValueType?>
{ }

public interface ITrackableValueEditable<TOwnerType, TMemberValueType, TValueType> : 
    ITrackableValueEditable<TMemberValueType?, TValueType?>, 
    ITrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{ }