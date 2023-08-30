namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

// TODO: Write Summaries
// TODO: Implement Unit Tests
public static class MemberValueTypeExtensions
{
    public static bool Equals<TMemberValueType>(
        this TMemberValueType value, IDirectionValueDiagnosis<TMemberValueType> directionValueDiagnosis) =>
        directionValueDiagnosis.Equals(value);
}