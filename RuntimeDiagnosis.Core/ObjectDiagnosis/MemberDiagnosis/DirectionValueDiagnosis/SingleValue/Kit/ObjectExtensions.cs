namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.SingleValue.Kit;

public static class ObjectExtensions
{
    public static bool Equals(this object obj, ISingleValue singleValue) =>
        singleValue.Equals(obj);
}