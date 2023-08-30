namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

public static class MemberValueTypeExtensions
{
    public static bool Equals<TMemberValueType>(
        this TMemberValueType value, IDirectionValueDiagnosis<TMemberValueType> directionValueDiagnosis) =>
        directionValueDiagnosis.Equals(value);
}