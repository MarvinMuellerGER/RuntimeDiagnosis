namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.SingleValue.Kit;

public static class ValueTypeExtensions
{
    public static bool Equals<TValueType>(this TValueType value, ISingleValue<TValueType> singleValue) =>
        singleValue.Equals(value);
}