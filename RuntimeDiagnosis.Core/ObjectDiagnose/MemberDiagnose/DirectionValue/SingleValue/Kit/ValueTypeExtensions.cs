namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.SingleValue.Kit;

public static class ValueTypeExtensions
{
    public static bool Equals<TValueType>(this TValueType value, ISingleValue<TValueType> singleValue) =>
        singleValue.Equals(value);
}