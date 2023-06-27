namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

public static class ObjectExtensions
{
    public static bool Equals(this object obj, IDirectionValueDiagnosis directionValueDiagnosis) =>
        directionValueDiagnosis.Equals(obj);
}