namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.SingleValue;

public interface ISingleValueAlwaysEditable: ISingleValue
{ }

public interface ISingleValueAlwaysEditable<TValueType>: ISingleValueAlwaysEditable, ISingleValue<TValueType?>
{
    new TValueType? Value { get; set; }
    void SetValue(TValueType? value, bool setAgainEvenIfNotChanged = false);
}

public interface ISingleValueAlwaysEditable<TMemberValueType, TValueType> : 
    ISingleValueAlwaysEditable<TValueType?>, ISingleValue<TMemberValueType?, TValueType?>
{ }

public interface ISingleValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType> : 
    ISingleValueAlwaysEditable<TMemberValueType?, TValueType?>, ISingleValue<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{ }