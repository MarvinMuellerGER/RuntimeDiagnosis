namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public interface ITrackableValueAlwaysEditableInternal<TOwnerType, TMemberValueType, TValueType> :
    ITrackableValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType>,
    ITrackableValueInternal<TOwnerType, TMemberValueType, TValueType>
    where TOwnerType : IDiagnosableObject
{ }