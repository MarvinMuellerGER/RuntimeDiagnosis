namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public interface ITrackableValueAlwaysEditable: ITrackableValue
{ }

public interface ITrackableValueAlwaysEditable<TValueType>: ITrackableValueAlwaysEditable, ITrackableValue<TValueType?>
{
    new TValueType? Value { get; set; }
}

public interface ITrackableValueAlwaysEditable<TMemberValueType, TValueType> : 
    ITrackableValueAlwaysEditable<TValueType?>, ITrackableValue<TMemberValueType?, TValueType?>
{ }

public interface ITrackableValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType> : 
    ITrackableValueAlwaysEditable<TMemberValueType?, TValueType?>, ITrackableValue<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{ }