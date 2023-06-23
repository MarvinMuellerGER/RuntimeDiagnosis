namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

public static class ObjectExtensions
{
    public static bool Equals(this object obj, IDirectionValue directionValue) =>
        directionValue.Equals(obj);
}