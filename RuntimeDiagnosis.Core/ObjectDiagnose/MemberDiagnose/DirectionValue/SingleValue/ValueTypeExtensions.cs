namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.SingleValue;

public static class ValueTypeExtensions
{
    public static bool Equals<TValueType>(this TValueType value, ISingleValue<TValueType> singleValue) =>
        singleValue.Equals(value);
}