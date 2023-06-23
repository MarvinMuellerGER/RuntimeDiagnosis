namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;

public static class MemberValueTypeExtensions
{
    public static bool Equals<TMemberValueType>(
        this TMemberValueType value, IDirectionValue<TMemberValueType> directionValue) =>
        directionValue.Equals(value);
}