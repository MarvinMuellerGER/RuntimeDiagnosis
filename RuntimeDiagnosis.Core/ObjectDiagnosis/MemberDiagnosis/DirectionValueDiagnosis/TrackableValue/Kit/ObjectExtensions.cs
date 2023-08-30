namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue.Kit;

// TODO: Write Summaries
// TODO: Implement Unit Tests
public static class ObjectExtensions
{
    public static bool Equals(this object obj, ITrackableValue trackableValue) =>
        trackableValue.Equals(obj);
}