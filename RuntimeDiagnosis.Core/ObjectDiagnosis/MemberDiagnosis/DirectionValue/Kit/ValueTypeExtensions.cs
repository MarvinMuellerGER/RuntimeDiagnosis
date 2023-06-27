namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;

public static class MemberValueTypeExtensions
{
    public static bool Equals<TMemberValueType>(
        this TMemberValueType value, IDirectionValue<TMemberValueType> directionValue) =>
        directionValue.Equals(value);
}