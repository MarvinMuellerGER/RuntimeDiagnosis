namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue.Kit;

public static class ObjectExtensions
{
    public static bool Equals(this object obj, ITrackableValue trackableValue) =>
        trackableValue.Equals(obj);
}