namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;

public static class ObjectExtensions
{
    public static bool Equals(this object obj, IDirectionValue directionValue) =>
        directionValue.Equals(obj);
}