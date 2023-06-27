namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.SingleValue;

public interface ISingleValueEditable : ISingleValueAlwaysEditable
{
    bool EditingCurrentlyAllowed { get; }
}

public interface ISingleValueEditable<TValueType> : ISingleValueEditable, ISingleValueAlwaysEditable<TValueType?>
{ }

public interface ISingleValueEditable<TMemberValueType, TValueType> : 
    ISingleValueEditable<TValueType?>, ISingleValueAlwaysEditable<TMemberValueType?, TValueType?>
{ }

public interface ISingleValueEditable<TOwnerType, TMemberValueType, TValueType> : 
    ISingleValueEditable<TMemberValueType?, TValueType?>, 
    ISingleValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{ }