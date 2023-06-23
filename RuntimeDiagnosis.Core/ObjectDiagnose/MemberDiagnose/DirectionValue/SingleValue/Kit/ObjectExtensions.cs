namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.SingleValue.Kit;

public static class ObjectExtensions
{
    public static bool Equals(this object obj, ISingleValue singleValue) =>
        singleValue.Equals(obj);
}