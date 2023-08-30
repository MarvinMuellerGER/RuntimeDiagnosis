namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue.Kit;

// TODO: Write Summaries
// TODO: Implement Unit Tests
public static class ValueTypeExtensions
{
    public static bool Equals<TValueType>(this TValueType value, ITrackableValue<TValueType> trackableValue) =>
        trackableValue.Equals(value);
}