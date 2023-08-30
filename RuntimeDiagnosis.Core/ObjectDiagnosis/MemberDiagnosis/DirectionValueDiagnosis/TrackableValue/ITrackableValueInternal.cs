namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

public interface ITrackableValueInternal<TValueType> : ITrackableValue<TValueType>
{
    internal void SetValue(TValueType? value, bool setAgainEvenIfNotChanged = false);
}

public interface ITrackableValueInternal<TMemberValueType, TValueType> : 
    ITrackableValueInternal<TValueType>, ITrackableValue<TMemberValueType, TValueType>
{ }

public interface ITrackableValueInternal<TOwnerType, TMemberValueType, TValueType> :
    ITrackableValueInternal<TMemberValueType, TValueType>, ITrackableValue<TOwnerType, TMemberValueType, TValueType>
    where TOwnerType : IDiagnosableObject
{
    internal void Initialize(
        IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis, string name);
}